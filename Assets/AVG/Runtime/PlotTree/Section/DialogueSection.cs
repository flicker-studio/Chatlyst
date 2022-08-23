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
    public class DialogueSection : Section
    {
        private List<Dialogue> dialogues { get; set; }
        public string characterName { get; set; }
        public string dialogueText { get; set; }
        public Rect nodePos { get; set; }

        public DialogueSection()
        {
        }
    }
}