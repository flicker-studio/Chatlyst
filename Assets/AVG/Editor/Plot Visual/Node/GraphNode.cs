using AVG.Runtime.ExtensionMethod;
using AVG.Runtime.PlotTree;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace AVG.Editor.Plot_Visual
{
    internal interface IGraphNode
    {
        public string Guid { get; }
    }

    internal abstract class GraphNode<T> : Node, IGraphNode
        where T : Section
    {
        public T Section;
        public string Guid => Section.Guid;
        protected VisualElement VisualElement;

        /// <summary>
        /// create a visual node base on .uxml
        /// </summary>
        /// <param name="uxml">visual asset</param>
        /// <returns>visual element</returns>
        private protected virtual VisualElement CreatVisual(VisualTreeAsset uxml)
        {
            var visualElement = new VisualElement();
            uxml.CloneTree(visualElement);
            return visualElement;
        }

        protected abstract void NodeVisual();

        public static void NodeAdd(PlotGraphView graphView, Vector2 mousePos, GraphNode<T> node)
        {
            var rect = mousePos.ToNodePosition(graphView);
            node.SetPosition(rect);
            node.mainContainer.Add(node.VisualElement);
            node.NodeVisual();
            graphView.AddElement(node);
        }

        public static GraphNode<T> NodeRedraw(PlotGraphView graphView, GraphNode<T> node)
        {
            //node.Section = section;
            var rect = node.Section.Pos.position.ToNodePosition(graphView);
            node.SetPosition(rect);
            node.mainContainer.Add(node.VisualElement);
            node.NodeVisual();
            graphView.AddElement(node);
            return node;
        }
    }
}