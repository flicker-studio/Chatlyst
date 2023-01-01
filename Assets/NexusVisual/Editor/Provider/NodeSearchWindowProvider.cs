using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace NexusVisual.Editor
{
    public class NodeSearchWindowProvider : ScriptableObject, ISearchWindowProvider
    {
        private PlotSoGraphView _plotSoGraphView;
        private EditorWindow _window;

        public void Init(PlotSoGraphView graphView, EditorWindow window)
        {
            _plotSoGraphView = graphView;
            _window = window;
        }

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            var types = typeof(BaseNvNode<>).Assembly.GetTypes();
            var nodeTypes = types.Where(a => a.GetInterfaces().Contains(typeof(IVisible))).ToArray();

            var tree = new List<SearchTreeEntry>
            {
                new SearchTreeGroupEntry(new GUIContent("Create Node")),
                new SearchTreeGroupEntry(new GUIContent("Nodes"), 1),
            };

            if (nodeTypes is not { Length: > 0 }) return tree;
            //Create corresponding buttons based on all classes that inherit IVisible interface
            tree.AddRange(nodeTypes.Select(t =>
            {
                var entry = new SearchTreeEntry(new GUIContent(t.Name))
                {
                    level = 2,
                    userData = t.FullName
                };
                return entry;
            }));

            return tree;
        }


        public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
        {
            var nodeRect = new Rect(context.screenMousePosition - _window.position.position, Vector2.one);
            var editorAssembly = typeof(BaseNvNode<>).Assembly;
            var typeName = (string)searchTreeEntry.userData;
            //Use C# Reflection to creat the node 
            if (editorAssembly.CreateInstance(typeName, false, BindingFlags.CreateInstance, null,
                    new object[] { null, nodeRect }, null, null) is not Node newNode) return false;
            _plotSoGraphView.AddElement(newNode);
            return true;
        }
    }
}