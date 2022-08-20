using System;
using AVG.Runtime.PlotTree;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

//TODO:Replace the uxml 
namespace AVG.Editor.Plot_Visual
{
    public class SectionNode : Node
    {
        public SectionData SectionData;
        public readonly PlotVisualElement PlotVisualElement;

        public SectionNode(SectionData baseData = null)
        {
            SectionData = new SectionData(baseData);
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