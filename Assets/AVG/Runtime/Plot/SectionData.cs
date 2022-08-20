using System;
using UnityEngine;

namespace AVG.Runtime.Plot
{
    [Serializable]
    public class SectionData
    {
        public string guid;
        public string characterName;
        public string dialogueText;

        public Rect nodePos;

        public SectionData()
        {
            guid = Guid.NewGuid().ToString();
        }

        public SectionData(SectionData originData, Rect editorPos)
        {
            guid = originData.guid;
            characterName = originData.characterName;
            dialogueText = originData.dialogueText;
            nodePos = editorPos;
        }
    }
}