using System;
using System.Collections.Generic;

namespace NexusVisual.Runtime
{
    [Serializable]
    public struct Dialogue
    {
        public CharacterName name;
        public string text;

        public string ToSentence() => name + ":" + text;
    }

    [Serializable]
    public class DialogueNvData : BaseNvData
    {
        public List<Dialogue> dialogueList = new List<Dialogue>();
    }
}