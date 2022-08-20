using System.Collections.Generic;
using AVG.Runtime.PlotTree;
using UnityEngine;

//TODO:Decouple editor and runtime
namespace AVG.Editor.Plot_Visual
{
    [CreateAssetMenu(fileName = "New Plot", menuName = "AVG/Creat Plot")]
    public class PlotSo : ScriptableObject
    {
        public List<NodeLink> links;

        public List<SectionData> nodes;


        public void ResetPlot()
        {
            links.Clear();
            nodes.Clear();
        }

        public SectionData GetStartNode()
        {
            throw new System.NotImplementedException();
        }

        public SectionData GetCurrentNode(string nodeGuid)
        {
            throw new System.NotImplementedException();
        }

        public SectionData GetNextNode(string nodeGuid)
        {
            throw new System.NotImplementedException();
        }
    }
}