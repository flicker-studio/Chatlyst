using System;

namespace AVG.Runtime.Plot
{
    [Serializable]
    public class SectionData
    {
        public string guid;
        public string characterName;
        public string dialogueText;

        public SectionData()
        {
            guid = Guid.NewGuid().ToString();
        }

        public SectionData(SectionData originData)
        {
            guid = originData.guid;
            characterName = originData.characterName;
            dialogueText = originData.dialogueText;
        }
    }
}