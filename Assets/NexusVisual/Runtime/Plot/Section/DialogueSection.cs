using System;
using System.Collections.Generic;
using UnityEngine;

namespace NexusVisual.Runtime
{
    [Serializable]
    public struct Dialogue
    {
        [DisplayOnly] public string name;
        [DisplayOnly] public string text;
    }

    [Serializable]
    public class DialogueSection : BaseSection
    {
        [Header("Dialogue")]
        public List<Dialogue> dialogues = new List<Dialogue>();
        [DisplayOnly] public string characterName;
        [DisplayOnly] public string dialogueText;
    }
}