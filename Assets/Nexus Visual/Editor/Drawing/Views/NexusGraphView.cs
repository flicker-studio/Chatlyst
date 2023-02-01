using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace NexusVisual.Editor.Views
{
    public class NexusGraphView : GraphView
    {
        private const KeyCode MenuKey = KeyCode.Space;
        private EditorWindow _window;

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
        }

        public void GraphInitialize(EditorWindow window)
        {
            // Add(_inspector);
            _window = window;
            RegisterCallback<KeyDownEvent>(SearchTreeBuild);
        }


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


        public List<NexusJsonEntry> NodeToEntryList()
        {
            var list = new List<NexusJsonEntry>();
            var nodeViewList = graphElements.Where(a => a is NexusNodeView).Cast<NexusNodeView>().ToList();
            foreach (var view in nodeViewList)
            {
                view.DataRefresh();

                list.Add(view.dataEntry);
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