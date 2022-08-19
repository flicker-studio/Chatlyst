using System;
using AVG.Runtime.Plot;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace AVG.Editor.Plot_Visual
{
    public class SectionNode : Node
    {
        public readonly SectionData SectionData;
        public readonly PlotVisualElement PlotVisualElement;

        public SectionNode(SectionData baseData = null)
        {
            SectionData = baseData ?? new SectionData();
            var visualAsset = EditorGUIUtility.Load("GraphNode.uxml") as VisualTreeAsset;
            PlotVisualElement = new PlotVisualElement(this, visualAsset);
        }
    }

    [Serializable]
    public class NodeLink
    {
        public string guid;
        public string nextGuid;
        public int portId;
    }
}