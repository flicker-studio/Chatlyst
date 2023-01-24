using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace NexusVisual.Editor
{
    public class NexusGraphView : GraphView
    {
        private const KeyCode MenuKey = KeyCode.Space;
        private EditorWindow _window;

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

        public List<NexusJsonEntry> GraphJsonEntryList()
        {
            return new List<NexusJsonEntry>();
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

        public class Factory : UxmlFactory<NexusGraphView, UxmlTraits>
        {
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