using System;
using UnityEngine;

namespace AVG.Runtime.PlotTree
{
    public interface ISection
    {
        public string guid { get; }
        public string next { get; }
        public Rect pos { get; set; }
    }

    [Serializable]
    public abstract class Section : ISection
    {
        public string guid => currentGuid;
        public string next
        {
            get => nextGuid;
            set => nextGuid = value;
        }
        public Rect pos
        {
            get => nodePos;
            set => nodePos = value;
        }

        [SerializeField] private string currentGuid;
        [SerializeField] private string nextGuid;
        [SerializeField] [HideInInspector] private Rect nodePos;

        protected Section()
        {
            currentGuid = Guid.NewGuid().ToString();
            nextGuid = null;
            nodePos = new Rect();
        }
    }
}