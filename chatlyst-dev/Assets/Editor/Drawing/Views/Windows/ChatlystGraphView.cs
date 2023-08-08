using System;
using System.Collections.Generic;
using System.Linq;
using Chatlyst.Editor.Serialization;
using Chatlyst.Runtime;
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

        private const    KeyCode                  MenuKey = KeyCode.Space;
        private readonly InspectorBlackboard      _inspector;
        private          EditorWindow             _window;
        private readonly NodeSearchWindowProvider _searchWindowProvider;
        private          NodeIndex                _nodeIndex;

        public ChatlystGraphView()
        {
            Insert(0, new GridBackground());
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            _inspector            = new InspectorBlackboard();
            _searchWindowProvider = ScriptableObject.CreateInstance<NodeSearchWindowProvider>();
            _searchWindowProvider.Init(this, _window);
            Add(_inspector);
        }

        public void GraphInitialize(EditorWindow window)
        {
            _window = window;
            // RegisterCallback<KeyDownEvent>(SearchTreeBuild);
        }

        /// <summary>
        ///     Use C# Reflection to creat the node
        /// </summary>
        /// <param name="nodeRect">Node location</param>
        /// <param name="typeName">The name of the node type</param>
        /// <param name="creatMethodName">The name of the method that created the node</param>
        /// <returns>Whether the node was successfully generated</returns>
        public bool CreatNode(Rect nodeRect, string typeName, string creatMethodName = "CreateNewInstance")
        {
            var method = typeof(IVisible).GetMethod(creatMethodName, new[] { typeof(Rect) });
            if (method == null) throw new Exception("No corresponding method could be found!");
            var    editorAssembly = typeof(NodeView).Assembly;
            object instance       = editorAssembly.CreateInstance(typeName);
            if (instance is not NodeView newNode) throw new Exception("Instance type error!");
            method.Invoke(newNode, new object[] { nodeRect });
            AddElement(newNode);
            return true;
        }

        public bool BuildFromNodeIndex(NodeIndex index)
        { /*
            foreach (var entity in index.BeginNodes)
            {
                string viewTypeName  = typeof(NodeView).Name; //entity.userData;
                var    assembly      = typeof(NodeView).Assembly;
                object instancedView = assembly.CreateInstance(viewTypeName);
                var    method        = typeof(IVisible).GetMethod("RebuildInstance", new[] { typeof(NexusJsonEntity) });
                if (instancedView is not NodeView nodeView || method == null) return false;
                method.Invoke(nodeView, new object[] { entity });
            }
            */
            foreach (var node in index.BeginNodes)
            {
                var startNodeView = new StartNodeView();
                startNodeView.RebuildInstance(node);
                AddElement(startNodeView);
            }
            _nodeIndex = index;
            return true;
        }

        private void RebuildInstanceList()
        {
        }

        /// <summary>
        ///     Get the current node index data
        /// </summary>
        /// <returns>Current node index</returns>
        public NodeIndex GetNodeIndex()
        {
            var nodeViewList = graphElements.Where(a => a is NodeView).Cast<NodeView>().ToList();
            var nodeDataList = nodeViewList.Select(nodeView => nodeView.userData as BasicNode).ToList();
            _nodeIndex.Refresh(nodeDataList);
            return _nodeIndex;
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
