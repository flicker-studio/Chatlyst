using System;
using System.Collections.Generic;

namespace NexusVisual.Editor.Data
{
    public struct Dialogue
    {
        public string Name;
        public string Talk;

        public Dialogue(string src)
        {
            var array = src.Split(":");
            if (array.Length != 2)
            {
                throw new Exception("Src Error!");
            }

            Name = array[0];
            Talk = array[1];
        }
    }

    public class DialoguesNode : NexusNode
    {
        public List<Dialogue> DialogueList = new List<Dialogue>();
    }
}