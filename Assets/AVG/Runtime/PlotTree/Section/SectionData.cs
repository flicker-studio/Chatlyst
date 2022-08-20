using System;
using UnityEngine;

namespace AVG.Runtime.PlotTree
{
    [Serializable]
    public class SectionData : Section
    {
        public string characterName;
        public string dialogueText;
        public Rect nodePos;

        public SectionData(SectionData inData) : base(inData == null)
        {
            if (inData == null) return;
            Guid = inData.Guid;
            characterName = inData.characterName;
            dialogueText = inData.dialogueText;
            nodePos = inData.nodePos;
        }
    }
}