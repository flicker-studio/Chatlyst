using System.Collections.Generic;
using AVG.Runtime.VisualGraph;
using UnityEngine;

namespace AVG.Editor.VisualGraph
{
    [CreateAssetMenu(fileName = "New Plot", menuName = "AVG/Creat Plot")]
    public class PlotSo : ScriptableObject, IPlotSo
    {
        [HideInInspector] public List<NodeLink> links;
        [HideInInspector] public List<GraphNode> nodes;

        public GraphNode GetStartNode()
        {
            throw new System.NotImplementedException();
        }

        public GraphNode GetCurrentNode(string nodeGuid)
        {
            throw new System.NotImplementedException();
        }

        public GraphNode GetNextNode(string nodeGuid)
        {
            throw new System.NotImplementedException();
        }
    }
}