using AVG.Runtime;
using AVG.Runtime.ExtensionMethod;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace AVG.Editor
{
    internal interface IGraphNode
    {
        public string Guid { get; }
    }

    internal abstract class GraphNode<T> : Node, IGraphNode
        where T : BaseSection
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

        public static void NodeAdd(PlotSoGraphView soGraphView, Vector2 mousePos, GraphNode<T> node)
        {
            var rect = mousePos.ToNodePosition(soGraphView);
            node.SetPosition(rect);
            node.mainContainer.Add(node.VisualElement);
            node.NodeVisual();
            soGraphView.AddElement(node);
        }

        public static GraphNode<T> NodeRedraw(PlotSoGraphView soGraphView, GraphNode<T> node)
        {
            //node.BaseSection = section;
            var rect = node.Section.Pos.position.ToNodePosition(soGraphView);
            node.SetPosition(rect);
            node.mainContainer.Add(node.VisualElement);
            node.NodeVisual();
            soGraphView.AddElement(node);
            return node;
        }
    }
}