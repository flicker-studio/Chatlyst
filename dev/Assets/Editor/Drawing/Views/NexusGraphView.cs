using System.Collections.Generic;
using System.Linq;
using Chatlyst.Editor.Serialization;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Chatlyst.Editor.Views
{
    public class NexusGraphView : GraphView
    {
        private const KeyCode MenuKey = KeyCode.Space;
        private EditorWindow _window;
        private readonly InspectorBlackboard _inspector;

        public class Factory : UxmlFactory<NexusGraphView, UxmlTraits>
        {
        }

        public NexusGraphView()
        {
            Insert(0, new GridBackground());
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            _inspector = new InspectorBlackboard();
        }

        public void GraphInitialize(EditorWindow window)
        {
            Add(_inspector);
            _window = window;
            RegisterCallback<KeyDownEvent>(SearchTreeBuild);
        }

        public void InspectorNode()
        {
            _inspector.Inspector(selection.Count > 0 ? selection[0] : null);
        }

        public override Blackboard GetBlackboard() => _inspector;


        private void SearchTreeBuild(KeyDownEvent keyDownEvent)
        {
            if (keyDownEvent.keyCode != MenuKey) return;
            //create a search windows under the cursor
            var worldMousePosition = _window.position.position + keyDownEvent.originalMousePosition;
            var searchWindowContext = new SearchWindowContext(worldMousePosition);
            var searchWindowProvider = ScriptableObject.CreateInstance<NodeSearchWindowProvider>();
            searchWindowProvider.Init(this, _window);
            SearchWindow.Open(searchWindowContext, searchWindowProvider);
        }

        public bool BuildFromEntries(List<NexusJsonEntity> list)
        {
            foreach (var entity in list)
            {
                var viewTypeName = typeof(NexusNodeView).Name; //entity.userData;
                var assembly = typeof(NexusNodeView).Assembly;
                var instancedView = assembly.CreateInstance(viewTypeName);
                var method = typeof(IVisible).GetMethod("RebuildInstance", new[] { typeof(NexusJsonEntity) });
                if (instancedView is not NexusNodeView nodeView || method == null) return false;
                method.Invoke(nodeView, new object[] { entity });
            }

            return true;
        }

        public IEnumerable<NexusJsonEntity> NodeEntity()
        {
            var list = new List<NexusJsonEntity>();
            var nodeViewList = graphElements.Where(a => a is NexusNodeView).Cast<NexusNodeView>().ToList();
            foreach (var view in nodeViewList)
            {
                view.DataRefresh();
                list.Add(view.dataEntity);
            }

            return list;
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
    }
}