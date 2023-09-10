using System;
using System.Collections.Generic;
using System.Linq;
using Chatlyst.Editor.Serialization;
using Chatlyst.Runtime;
using Chatlyst.Runtime.Data;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Chatlyst.Editor
{
    public class ChatlystGraphView : GraphView
    {
        public class Factory : UxmlFactory<ChatlystGraphView, UxmlTraits>
        {
        }

        private const    KeyCode                  MenuKey = KeyCode.Space;
        private readonly InspectorBlackboard      _inspector;
        private readonly ChatlystEditorWindow     _window;
        private readonly NodeSearchWindowProvider _searchWindowProvider;
        private          NodeIndex                _nodeIndex;

        /// <summary>
        ///     Get the current node index data
        /// </summary>
        public NodeIndex nodeIndex
        {
            get
            {
                var nodeViewList = graphElements.Where(a => a is NodeView).Cast<NodeView>().ToList();
                foreach (var view in nodeViewList)
                {
                    view.RefreshData();
                }
                var nodeDataList = nodeViewList.Select(nodeView => nodeView.userData as BasicNode).ToList();
                _nodeIndex.Refresh(nodeDataList);
                return _nodeIndex;
            }
        }

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

        public void Initialize(string jsonData)
        {
            _searchWindowProvider.Init(this, _window);
            var nodeDataIndex = IndexJsonInternal.Deserialize(jsonData);
            if (nodeDataIndex == null) throw new Exception("Deserialize failed!");
            RebuildNodeViews(nodeDataIndex);
        }

        private bool RebuildNodeViews(NodeIndex index)
        {
            foreach (var startNodeView in index.BeginNodes.Select
                (node => NodeViewFactory.RebuildOldNodeView(node, typeof(BeginNodeView).FullName)))
            {
                AddElement(startNodeView);
            }
            _nodeIndex = index;
            return true;
        }

        /// <summary>
        ///     Create a search windows under the cursor
        /// </summary>
        /// <param name="keyDownEvent">Trigger event</param>
        private void SearchTreeBuild(KeyDownEvent keyDownEvent)
        {
            if (keyDownEvent.keyCode != MenuKey) return;
            var worldMousePosition  = _window.position.position + keyDownEvent.originalMousePosition;
            var searchWindowContext = new SearchWindowContext(worldMousePosition);
            SearchWindow.Open(searchWindowContext, _searchWindowProvider);
        }

        public override Blackboard GetBlackboard()
        {
            return _inspector;
        }

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
    }
}
