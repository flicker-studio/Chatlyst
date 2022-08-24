using System;
using UnityEngine;

namespace AVG.Runtime.PlotTree
{
    [Serializable]
    public struct Dialogue
    {
        public string name;
        public string text;
    }

    [Serializable]
    public class DialogueSection : Section
    {
        [Header("Dialogue")]
        // private List<Dialogue> dialogues ;
        public string characterName;
        public string dialogueText;
    }
}