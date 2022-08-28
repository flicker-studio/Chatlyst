using AVG.Runtime.PlotTree;
using UnityEditor;
using UnityEngine.UIElements;

namespace AVG.Editor.Plot_Visual
{
    public sealed class StartNode : GraphNode<StartSection>
    {
        public StartNode()
        {
            var visualAsset = EditorGUIUtility.Load("StartNode.uxml") as VisualTreeAsset;
            VisualElement = CreatVisual(visualAsset);
        }
    }
}