using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace AVG.Editor.Plot_Visual
{
    public interface IGraphNode
        // where T : ISection
    {
        //public Type type => typeof(T);
        public void NodeVisual();
    }

    public abstract class GraphNode : Node, IGraphNode
        // where T : ISection
    {
        public string Guid;
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

        public abstract void NodeVisual();
    }
}