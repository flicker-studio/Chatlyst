using AVG.Runtime.PlotTree;
using UnityEngine.UIElements;

namespace AVG.Editor.Plot_Visual
{
    public interface INode<in T> where T : Section
    {
        public VisualElement visual { get; set; }

        /// <summary>
        /// create a visual node base on .uxml
        /// </summary>
        /// <param name="uxml">visual asset</param>
        /// <returns>visual element</returns>
        public VisualElement CreatVisual(VisualTreeAsset uxml);
    }
}