using System.Collections.Generic;
using System.Linq;
using Chatlyst.Editor.Serialization;
using UnityEditor;
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

        private const    KeyCode             MenuKey = KeyCode.Space;
        private readonly InspectorBlackboard _inspector;
        private          EditorWindow        _window;

        public ChatlystGraphView()
        {
            Insert(0, new GridBackground());
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            _inspector = new InspectorBlackboard();
            Add(_inspector);
        }

        public void GraphInitialize(EditorWindow window)
        {
            _window = window;
            RegisterCallback<KeyDownEvent>(SearchTreeBuild);
        }

        public void InspectorNode()
        {
            _inspector.Inspector(selection.Count > 0 ? selection[0] : null);
        }


        private void SearchTreeBuild(KeyDownEvent keyDownEvent)
        {
            if (keyDownEvent.keyCode != MenuKey) return;
            //create a search windows under the cursor
            var worldMousePosition   = _window.position.position + keyDownEvent.originalMousePosition;
            var searchWindowContext  = new SearchWindowContext(worldMousePosition);
            var searchWindowProvider = ScriptableObject.CreateInstance<NodeSearchWindowProvider>();
            searchWindowProvider.Init(this, _window);
            SearchWindow.Open(searchWindowContext, searchWindowProvider);
        }

        public bool BuildFromEntries(List<NexusJsonEntity> list)
        {
            foreach (var entity in list)
            {
                string viewTypeName  = typeof(NodeView).Name; //entity.userData;
                var    assembly      = typeof(NodeView).Assembly;
                object instancedView = assembly.CreateInstance(viewTypeName);
                var    method        = typeof(IVisible).GetMethod("RebuildInstance", new[] { typeof(NexusJsonEntity) });
                if (instancedView is not NodeView nodeView || method == null) return false;
                method.Invoke(nodeView, new object[] { entity });
            }

            return true;
        }

        public IEnumerable<NexusJsonEntity> NodeEntity()
        {
            var list         = new List<NexusJsonEntity>();
            var nodeViewList = graphElements.Where(a => a is NodeView).Cast<NodeView>().ToList();
            foreach (var view in nodeViewList)
            {
                /*view.DataRefresh();
                list.Add(view.dataEntity);*/
            }

            return list;
        }

        #region Override
        public override Blackboard GetBlackboard()
        {
            return _inspector;
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            var compatiblePorts = new List<Port>();
            ports.ForEach(port =>
            {
                if (startPort != port && startPort.node != port.node)
                    compatiblePorts.Add(port);
            });

            return compatiblePorts;
        }
        #endregion
    }
}
