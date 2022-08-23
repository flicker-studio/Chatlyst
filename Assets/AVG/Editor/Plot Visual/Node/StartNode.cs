using AVG.Runtime.PlotTree;
using UnityEditor.Graphs;
using UnityEngine.UIElements;

namespace AVG.Editor.Plot_Visual
{
    public class StartNode : Node, INode<StartSection>
    {
        public VisualElement visual { get; set; }

        public VisualElement CreatVisual(VisualTreeAsset uxml)
        {
            throw new System.NotImplementedException();
        }
    }
}