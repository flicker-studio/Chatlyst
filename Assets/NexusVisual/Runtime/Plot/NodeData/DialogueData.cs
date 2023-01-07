using System;
using System.Collections.Generic;

namespace NexusVisual.Runtime
{
    [Serializable]
    public struct Dialogue
    {
        public CharacterName name;
        public string text;

        public string ToSentence()
        {
            return name + ":" + text;
        }
    }

    [Serializable]
    public class DialogueData : BaseData
    {
        public List<Dialogue> dialogueList = new List<Dialogue>();
        public Dialogue[] test;
    }
}