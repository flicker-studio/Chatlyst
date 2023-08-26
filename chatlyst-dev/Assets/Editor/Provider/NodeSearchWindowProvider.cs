using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Chatlyst.Editor
{
    public class NodeSearchWindowProvider : ScriptableObject, ISearchWindowProvider
    {
        private ChatlystGraphView    _graph;
        private ChatlystEditorWindow _window;

        public void Init(ChatlystGraphView graphView, ChatlystEditorWindow window)
        {
            _graph  = graphView;
            _window = window;
        }

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            var assemblyTypes  = typeof(NodeView).Assembly.GetTypes();
            var nodeTypesArray = assemblyTypes.Where(a => a.GetInterfaces().Contains(typeof(INodeView))).ToArray();

            var tree =
                new List<SearchTreeEntry>
                {
                    new SearchTreeGroupEntry(new GUIContent("Create Node")),
                    new SearchTreeGroupEntry(new GUIContent("Nodes"), 1)
                };

            if (nodeTypesArray is not { Length: > 0 }) return tree;
            //Create corresponding buttons based on all classes that inherit INodeView interface
            tree.AddRange
                (
                 from type in nodeTypesArray
                 let nameAttribute = type.GetCustomAttribute<SearchTreeNameAttribute>()
                 where nameAttribute != null
                 select new SearchTreeEntry(new GUIContent(nameAttribute.Name))
                        {
                            level    = 2,
                            userData = type.FullName
                        }
                );

            return tree;
        }

        public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
        {
            var    nodeRect = new Rect(context.screenMousePosition - _window.position.position, Vector2.one);
            string typeName = (string)searchTreeEntry.userData;
            var    newNode  = NodeViewFactory.CreatNewNodeView(nodeRect, typeName);
            if (newNode == null) return false;
            _graph.AddElement(newNode);
            return true;
        }
    }
}
