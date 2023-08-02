using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Chatlyst.Editor
{
    public class NodeSearchWindowProvider : ScriptableObject, ISearchWindowProvider
    {
        private GraphView    _graph;
        private EditorWindow _window;

        public void Init(GraphView graphView, EditorWindow window)
        {
            _graph  = graphView;
            _window = window;
        }

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            var types     = typeof(NodeView).Assembly.GetTypes();
            var nodeTypes = types.Where(a => a.GetInterfaces().Contains(typeof(IVisible))).ToArray();

            var tree = new List<SearchTreeEntry>
                       {
                           new SearchTreeGroupEntry(new GUIContent("Create Node")),
                           new SearchTreeGroupEntry(new GUIContent("Nodes"), 1),
                       };

            if (nodeTypes is not { Length: > 0 }) return tree;
            //Create corresponding buttons based on all classes that inherit IVisible interface
            tree.AddRange(
                          from type in nodeTypes
                          let displayAttribute = type.GetCustomAttribute<SearchTreeNameAttribute>()
                          where displayAttribute != null
                          select new SearchTreeEntry(new GUIContent(displayAttribute.Name))
                                 {
                                     level    = 2,
                                     userData = type.FullName
                                 });

            return tree;
        }

        public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
        {
            var nodeRect       = new Rect(context.screenMousePosition - _window.position.position, Vector2.one);
            var editorAssembly = typeof(NodeView).Assembly;
            var typeName       = (string)searchTreeEntry.userData;
            //Use C# Reflection to creat the node 
            var instance = editorAssembly.CreateInstance(typeName);
            var method   = typeof(IVisible).GetMethod("CreateInstance", new[] { typeof(Rect) });
            if (method == null || instance is not NodeView newNode) return false;
            method.Invoke(newNode, new object[] { nodeRect });
            _graph.AddElement(newNode);
            return true;
        }
    }
}
