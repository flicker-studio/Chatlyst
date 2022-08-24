using System;
using UnityEngine;

namespace AVG.Runtime.PlotTree
{
    public interface ISection
    {
        public string guid { get; set; }
        public string nextGuid { get; set; }
        public Rect nodePos { get; set; }
    }

    public abstract class Section : ISection
    {
        public string guid { get; set; }
        public string nextGuid { get; set; }
        public Rect nodePos { get; set; }

        protected Section()
        {
            guid = Guid.NewGuid().ToString();
            nextGuid = null;
            nodePos = new Rect();
        }
    }
}