using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NexusVisual.Runtime
{
    public interface ISection
    {
        public string Guid { get; }
        public string Next { get; set; }
        public Rect Pos { get; set; }
    }

    [Serializable]
    public abstract class BaseSection : ScriptableObject
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

        [SerializeField] [DisplayOnly] private string currentGuid;
        [SerializeField] [DisplayOnly] private string nextGuid;
        [SerializeField] [DisplayOnly] private Rect nodePos;

        protected BaseSection()
        {
            currentGuid = System.Guid.NewGuid().ToString();
            nextGuid = null;
            nodePos = new Rect();
        }
    }
}