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
        public StartSection StartSection;

        public List<DialogueSection> dialogueSections;

        public string seLength => dialogueSections.Count.ToString();
        public string startGuid => StartSection.guid;


        public void ResetPlot()
        {
            links?.Clear();
            dialogueSections?.Clear();
        }
    }
}