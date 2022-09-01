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
        public int seLength => dialogueSections.Count;

        //reset
        public void Reset()
        {
            startSection = null;
            dialogueSections?.Clear();
        }

        public Dictionary<string, ISection> ToDictionary()
        {
            var dic = new Dictionary<string, ISection>();
            foreach (var dialogue in dialogueSections)
            {
                dic.Add(dialogue.guid, dialogue);
            }

            dic.Add(startSection.guid, startSection);
            return dic;
        }
    }
}