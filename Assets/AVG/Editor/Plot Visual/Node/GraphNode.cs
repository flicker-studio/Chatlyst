using AVG.Runtime.ExtensionMethod;
using UnityEditor.Experimental.GraphView;
using AVG.Runtime.PlotTree;
using UnityEngine.UIElements;
using UnityEngine;

namespace AVG.Editor.Plot_Visual
{
    internal interface IGraphNode
    {
    }

    internal abstract class GraphNode<T> : Node, IGraphNode
        where T : Section
    {
        public T Section;
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

        public abstract void NodeVisual();

        public static void NodeAdd(PlotGraphView graphView, Vector2 mousePos, GraphNode<T> node)
        {
            var rect = mousePos.ToNodePosition(graphView);
            node.SetPosition(rect);
            node.mainContainer.Add(node.VisualElement);
            node.NodeVisual();
            graphView.AddElement(node);
        }

        public static GraphNode<T> NodeRedraw(PlotGraphView graphView, T section, GraphNode<T> node)
        {
            node.Section = section;
            var rect = section.pos.position.ToNodePosition(graphView);
            node.SetPosition(rect);
            node.mainContainer.Add(node.VisualElement);
            node.NodeVisual();
            graphView.AddElement(node);
            return node;
        }
    }
}