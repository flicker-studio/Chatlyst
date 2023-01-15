using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace NexusVisual.Runtime
{
    [CreateAssetMenu(fileName = "New Plot", menuName = "Nexus Visual/Creat Plot")]
    public class PlotSo : ScriptableObject
    {
        [SerializeField] [HideInInspector] public List<StartNvData> startDataList = new List<StartNvData>();
        [SerializeField] [HideInInspector] public List<DialogueNvData> dialogueDataList = new List<DialogueNvData>();

        private void ResetData()
        {
            startDataList?.Clear();
            dialogueDataList?.Clear();
        }

        /// <summary>
        /// The data is provided as a dictionary
        /// </summary>
        /// <exception cref="WarningException">
        /// Thrown when the assignment is empty, which means the saved data disappears
        /// </exception>
        [CanBeNull]
        public Dictionary<string, BaseNvData> nodesData
        {
            get
            {
                var dataList = new List<BaseNvData>();
                dataList.AddRange(startDataList);
                dataList.AddRange(dialogueDataList);
                return dataList.Count > 0 ? dataList.ToDictionary(sec => sec.guid) : new Dictionary<string, BaseNvData>();
            }

            set
            {
                if (value == null) throw new WarningException("Value is NULL,please check!");
                ResetData();
                foreach (var section in value.Values)
                {
                    switch (section)
                    {
                        case StartNvData start:
                            if (!startDataList.Contains(start)) startDataList.Add(start);
                            break;
                        case DialogueNvData dialogue:
                            if (!dialogueDataList.Contains(dialogue)) dialogueDataList.Add(dialogue);
                            break;
                        default:
                            Debug.Log("Unknown BaseNvData");
                            break;
                    }
                }
            }
        }
    }
}