using System.Collections.Generic;
using UnityEngine;

//TODO:Decouple editor and runtime
namespace AVG.Runtime.PlotTree
{
    [CreateAssetMenu(fileName = "New Plot", menuName = "AVG/Creat Plot")]
    public class PlotSo : ScriptableObject
    {
        public StartSection startSection;
        public List<DialogueSection> dialogueSections;

        public readonly Dictionary<string, int> DialogueSectionDictionary = new();
        public int seLength => dialogueSections.Count;

        public void ResetPlot()
        {
            startSection = null;
            dialogueSections?.Clear();
            DialogueSectionDictionary?.Clear();
        }
    }
}