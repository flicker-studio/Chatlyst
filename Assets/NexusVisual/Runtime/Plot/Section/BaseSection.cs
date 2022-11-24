using System;
using UnityEngine;

namespace NexusVisual.Runtime
{
    public interface ISection
    {
        public string Guid { get; }
        public string Next { get; set; }
        public Rect Pos { get; set; }
    }

    [Serializable]
    public abstract class BaseSection
    {
        public string Guid => currentGuid;
        public string Next
        {
            get => nextGuid;
            set => nextGuid = value;
        }
        public Rect Pos
        {
            get => nodePos;
            set => nodePos = value;
        }

        [SerializeField] [HideInInspector] private string currentGuid;
        [SerializeField] [HideInInspector] private string nextGuid;
        [SerializeField] [HideInInspector] private Rect nodePos;

        protected BaseSection()
        {
            currentGuid = System.Guid.NewGuid().ToString();
            nextGuid = null;
            nodePos = new Rect();
        }
    }
}