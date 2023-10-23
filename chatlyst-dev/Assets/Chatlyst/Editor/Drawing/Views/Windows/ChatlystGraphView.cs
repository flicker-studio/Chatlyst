#region
using System;
using System.Collections.Generic;
using System.Linq;
using Chatlyst.Editor.Serialization;
using Chatlyst.Runtime.Data;
using Chatlyst.Runtime.Serialization;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
#endregion

namespace Chatlyst.Editor
{
    /// <summary>
    ///     Custom Graph View
    /// </summary>
    public class ChatlystGraphView : GraphView
    {
        private const    KeyCode                  MenuKey = KeyCode.Space;
        private readonly InspectorBlackboard      _inspector;
        private readonly NodeSearchWindowProvider _searchWindowProvider;
        private readonly ChatlystEditorWindow     _window;
        private          NodeDataIndex            _nodeDataIndex;

        /// <summary>
        ///     Generate a list of nodes based on the value of <see cref="_nodeDataIndex" />
        /// </summary>
        private void RebuildNodeViews()
        {
            var startNodeDataList = _nodeDataIndex.BeginNodesList;

            foreach (var startNodeData in startNodeDataList)

            {
                var startNodeView = NodeViewFactory.RebuildOldNodeView(startNodeData, typeof(BeginNodeView).FullName);
                AddElement(startNodeView);
            }
        }

        /// <summary>
        ///     Create a search windows under the cursor
        /// </summary>
        /// <param name="keyDownEvent">Trigger event,the default is a space</param>
        private void SearchTreeBuild(KeyDownEvent keyDownEvent)
        {
            if (keyDownEvent.keyCode != MenuKey) return;

            var worldMousePosition  = _window.position.position + keyDownEvent.originalMousePosition;
            var searchWindowContext = new SearchWindowContext(worldMousePosition);
            SearchWindow.Open(searchWindowContext, _searchWindowProvider);
        }

        /// <inheritdoc />
        public class Factory : UxmlFactory<ChatlystGraphView, UxmlTraits>
        {
        }

        #region API
        /// <summary>
        ///     Get the current node dataIndex data
        /// </summary>
        public NodeDataIndex nodeDataIndex
        {
            get
            {
                var nodeViewList = graphElements.Where(a => a is NodeView).Cast<NodeView>().ToList();

                foreach (var view in nodeViewList)
                {
                    view.RefreshData();
                }
                var nodeDataList = nodeViewList.Select(nodeView => nodeView.userData as BasicNode).ToList();
                _nodeDataIndex.Refresh(nodeDataList);
                return _nodeDataIndex;
            }
        }

        /// <summary>
        ///     Default constructor
        /// </summary>
        public ChatlystGraphView()
        {
            Insert(0, new GridBackground());
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            _inspector            = new InspectorBlackboard();
            _searchWindowProvider = ScriptableObject.CreateInstance<NodeSearchWindowProvider>();
            RegisterCallback<KeyDownEvent>(SearchTreeBuild);
            Add(_inspector);
            _window = ChatlystEditorWindow.EditorWindow;
        }

        /// <summary>
        ///     Initialize graph and deserialize the string to a Node View
        /// </summary>
        /// <param name="jsonData">The string read</param>
        /// <exception cref="Exception">Deserialization error</exception>
        public void Initialize(string jsonData)
        {
            _searchWindowProvider.Init(this, _window);

            try
            {
                _nodeDataIndex = IndexJsonInternal.Deserialize(jsonData);
                RebuildNodeViews();
            }
            catch
            {
                throw new Exception("Deserialize failed!");
            }
        }
        #endregion

        #region Override
        /// <inheritdoc />
        public override Blackboard GetBlackboard()
        {
            return _inspector;
        }

        /// <inheritdoc />
        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            var compatiblePorts = new List<Port>();

            ports.ForEach
                (
                 port =>
                 {
                     if (startPort != port && startPort.node != port.node)
                         compatiblePorts.Add(port);
                 }
                );
            return compatiblePorts;
        }
        #endregion
    }
}
