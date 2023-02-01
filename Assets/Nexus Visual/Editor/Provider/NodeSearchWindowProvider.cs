using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NexusVisual.Editor.Views;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace NexusVisual.Editor
{
    public class NodeSearchWindowProvider : ScriptableObject, ISearchWindowProvider
    {
        private GraphView _graph;
        private EditorWindow _window;

        public void Init(GraphView graphView, EditorWindow window)
        {
            _graph = graphView;
            _window = window;
        }

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            var types = typeof(NexusNodeView).Assembly.GetTypes();
            var nodeTypes = types.Where(a => a.GetInterfaces().Contains(typeof(IVisible))).ToArray();

            var tree = new List<SearchTreeEntry>
            {
                new SearchTreeGroupEntry(new GUIContent("Create Node")),
                new SearchTreeGroupEntry(new GUIContent("Nodes"), 1),
            };

            if (nodeTypes is not { Length: > 0 }) return tree;
            //Create corresponding buttons based on all classes that inherit IVisible interface
            foreach (var type in nodeTypes)
            {
                var displayAttribute = type.GetCustomAttribute<SearchTreeNameAttribute>();
                if (displayAttribute == null) continue;
                var entry = new SearchTreeEntry(new GUIContent(displayAttribute.Name))
                {
                    level = 2,
                    userData = type.FullName
                };
                tree.Add(entry);
            }

            return tree;
        }

        public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
        {
            var nodeRect = new Rect(context.screenMousePosition - _window.position.position, Vector2.one);
            var editorAssembly = typeof(NexusNodeView).Assembly;
            var typeName = (string)searchTreeEntry.userData;
            //Use C# Reflection to creat the node 
            if (editorAssembly.CreateInstance(typeName, false, BindingFlags.CreateInstance, null,
                    new object[] { nodeRect }, null, null) is not Node newNode) return false;
            _graph.AddElement(newNode);
            return true;
        }
    }
}