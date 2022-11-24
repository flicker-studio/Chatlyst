using System;
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
        // private List<Dialogue> dialogues ;
        [DisplayOnly] public string characterName;
        [DisplayOnly] public string dialogueText;
    }
}