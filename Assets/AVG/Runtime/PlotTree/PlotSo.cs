using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//TODO:Decouple editor and runtime
namespace AVG.Runtime.PlotTree
{
    [Serializable]
    public class SectionCollection
    {
        public List<StartSection> startSections = new List<StartSection>();
        public List<DialogueSection> dialogueSections = new List<DialogueSection>();

        public void Reset()
        {
            startSections?.Clear();
            dialogueSections?.Clear();
        }

        public Dictionary<string, ISection> ToDictionary()
        {
            var dic = dialogueSections.ToDictionary<DialogueSection, string, ISection>(
                dialogue => dialogue.Guid, dialogue => dialogue);

            foreach (var start in startSections)
            {
                dic.Add(start.Guid, start);
            }

            return dic;
        }

        public SectionCollection(Dictionary<string, ISection> dictionary)
        {
            foreach (var section in dictionary.Values)
            {
                switch (section)
                {
                    case StartSection start:
                        if (!startSections.Contains(start)) startSections.Add(start);
                        break;
                    case DialogueSection dialogue:
                        if (!dialogueSections.Contains(dialogue)) dialogueSections.Add(dialogue);
                        break;
                    default:
                        Debug.Log("Unknown Section");
                        break;
                }
            }
        }

        public SectionCollection()
        {
        }
    }

    [CreateAssetMenu(fileName = "New Plot", menuName = "AVG/Creat Plot")]
    public class PlotSo : ScriptableObject
    {
        public SectionCollection sectionCollection;
    }
}