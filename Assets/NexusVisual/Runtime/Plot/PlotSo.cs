using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NexusVisual.Runtime
{
    [CreateAssetMenu(fileName = "New Plot", menuName = "Nexus Visual/Creat Plot")]
    public class PlotSo : ScriptableObject
    {
        [SerializeField] [HideInInspector] public List<StartData> startSections = new List<StartData>();
        [SerializeField] [HideInInspector] public List<DialogueData> dialogueSections = new List<DialogueData>();

        private void ResetData()
        {
            startSections?.Clear();
            dialogueSections?.Clear();
        }

        public Dictionary<string, BaseData> nodesData
        {
            get
            {
                var sectionList = new List<BaseData>();
                sectionList.AddRange(startSections);
                sectionList.AddRange(dialogueSections);
                return sectionList.ToDictionary(sec => sec.guid);
            }
            set
            {
                ResetData();
                foreach (var section in value.Values)
                {
                    switch (section)
                    {
                        case StartData start:
                            if (!startSections.Contains(start)) startSections.Add(start);
                            break;
                        case DialogueData dialogue:
                            if (!dialogueSections.Contains(dialogue)) dialogueSections.Add(dialogue);
                            break;
                        default:
                            Debug.Log("Unknown BaseData");
                            break;
                    }
                }
            }
        }
    }
}