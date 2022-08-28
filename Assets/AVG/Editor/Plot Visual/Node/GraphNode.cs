using System;
using AVG.Runtime.PlotTree;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace AVG.Editor.Plot_Visual
{
    public interface IGraphNode<T>
        where T : ISection
    {
        public Type type => typeof(T);
    }

    public abstract class GraphNode<T> : Node, IGraphNode<T>
        where T : ISection
    {
        public T Section;
        public VisualElement VisualElement;

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
    }
}