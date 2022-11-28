using System;
using System.Linq;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace NexusVisual.Editor
{
    public class NodeSearchWindowProvider : ScriptableObject, ISearchWindowProvider
    {
        private Type[] _nodeTypes;
        private PlotSoGraphView _plotSoGraphView;

        public void Info(PlotSoGraphView graphView)
        {
            _plotSoGraphView = graphView;
        }

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            var types = typeof(BaseNvNode<>).Assembly.GetTypes();
            _nodeTypes = types.Where(a => a.GetInterfaces().Contains(typeof(IVisible))).ToArray();
            var tree = new List<SearchTreeEntry>
            {
                new SearchTreeGroupEntry(new GUIContent("Create Node"), 0),
                new SearchTreeGroupEntry(new GUIContent("Nodes"), 1),
            };

            if (_nodeTypes is not { Length: > 0 }) return tree;
            //根据所有继承了IVisible接口的类，创建对应的按钮
            tree.AddRange(_nodeTypes.Select(t =>
            {
                var entry = new SearchTreeEntry(new GUIContent(t.Name))
                {
                    level = 2,
                    userData = t.Name
                };
                return entry;
            }));

            return tree;
        }


        public bool OnSelectEntry(SearchTreeEntry searchTreeEntry, SearchWindowContext context)
        {
            var graphMousePosition = _plotSoGraphView.LocalToWorld(Event.current.mousePosition);

            var editorAssembly = typeof(BaseNvNode<>).Assembly;
            var typeName = (string)searchTreeEntry.userData;
            switch (typeName)
            {
                case "StartNode":
                    var a = new StartNode(targetPos: new Rect(graphMousePosition, Vector2.one));
                    _plotSoGraphView.AddElement(a);
                    break;
                case "DialogueNode":
                    var node = new DialogueNode(targetPos: new Rect(graphMousePosition, Vector2.one));
                    _plotSoGraphView.AddElement(node);
                    break;
                default:
                    break;
            }
/*

            var newNode = editorAssembly.CreateInstance(typeName);
            if (newNode == null) return false;
            var nodeType = newNode.GetType();
            nodeType.GetMethod("NodeAdd")
                ?.Invoke(newNode, new[] { _plotSoGraphView, graphMousePosition, newNode });*/

            return true;
        }
    }
}