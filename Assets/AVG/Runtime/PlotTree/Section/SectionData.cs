using System;
using System.Collections.Generic;
using UnityEngine;

namespace AVG.Runtime.PlotTree
{
    [Serializable]
    public struct Dialogue
    {
        public string name { get; set; }
        public string text { get; set; }
    }

    [Serializable]
    public class SectionData : ISection
    {
        public string guid { get; set; }
        public string nextGuid { get; set; }
        private List<Dialogue> dialogues { get; set; }
        public string characterName { get; set; }
        public string dialogueText { get; set; }
        public Rect nodePos { get; set; }

        public SectionData(SectionData inData = null)
        {
            if (inData == null)
            {
                guid = Guid.NewGuid().ToString();
                nextGuid = null;
                return;
            }

            guid = inData.guid;
            nextGuid = inData.nextGuid;
            characterName = inData.characterName;
            dialogueText = inData.dialogueText;
            dialogues = inData.dialogues;
            nodePos = inData.nodePos;
        }
    }
}