using AVG.Runtime.PlotTree;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace AVG.Editor.Plot_Visual
{
    public interface IGraphNode
    {
        public void NodeVisual();
    }

    public abstract class GraphNode<T> : Node, IGraphNode
        where T : Section
    {
        public T Section;
        protected VisualElement VisualElement;

        /// <summary>
        /// create a visual node base on .uxml
        /// </summary>
        /// <param name="uxml">visual asset</param>
        /// <returns>visual element</returns>
        protected virtual VisualElement CreatVisual(VisualTreeAsset uxml)
        {
            var visualElement = new VisualElement();
            uxml.CloneTree(visualElement);
            return visualElement;
        }

        public abstract void NodeVisual();

        public static void NodeAdd(PlotGraphView graphView, Vector2 mousePos, GraphNode<T> node)
        {
            var rect = graphView.ToNodePosition(mousePos);
            node.SetPosition(rect);
            node.mainContainer.Add(node.VisualElement);
            node.NodeVisual();
            graphView.AddElement(node);
        }

        public static GraphNode<T> NodeRedraw(PlotGraphView graphView, T section, GraphNode<T> node)
        {
            node.Section = section;
            var rect = graphView.ToNodePosition(section.pos.position);
            node.SetPosition(rect);
            node.mainContainer.Add(node.VisualElement);
            node.NodeVisual();
            graphView.AddElement(node);
            return node;
        }
    }
}