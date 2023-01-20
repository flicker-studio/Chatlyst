using System;
using UnityEngine;

namespace NexusVisual.Runtime
{
    /// <summary>
    /// Used to provide the basic properties of the nvnode
    /// </summary>
    [Serializable]
    public class BaseNvData : ScriptableObject
    {
        /// <summary>
        /// The guid of the current nvnode is globally unique and cannot be changed
        /// </summary>
        public string guid
        {
            get => currentGuid;
            set => currentGuid = value;
        }

        [SerializeField] private string currentGuid;
        [SerializeField] public string nextGuid;
        [SerializeField] public Rect nodePos;

        protected BaseNvData()
        {
            currentGuid = Guid.NewGuid().ToString();
            nextGuid = null;
            nodePos = new Rect();
        }
    }
}