using System;
using System.Collections.Generic;
using UnityEngine;

//TODO:Decouple editor and runtime
namespace AVG.Runtime.PlotTree
{
    [Serializable]
    public class NodeLink
    {
        public string guid;
        public string nextGuid;
    }

    [CreateAssetMenu(fileName = "New Plot", menuName = "AVG/Creat Plot")]
    public class PlotSo : ScriptableObject
    {
        public List<NodeLink> links;

        public List<Section> nodes;

        public string startGuid;

        public void ResetPlot()
        {
            links?.Clear();
            nodes?.Clear();
        }
    }
}