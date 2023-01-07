using System;
using UnityEngine;

namespace NexusVisual.Runtime
{
    [Serializable]
    public class BaseData : ScriptableObject
    {
        public string guid => currentGuid;

        //Data Field
        [SerializeField] private string currentGuid;
        [SerializeField] public string nextGuid;
        [SerializeField] public Rect nodePos;

        protected BaseData()
        {
            currentGuid = Guid.NewGuid().ToString();
            nextGuid = null;
            nodePos = new Rect();
        }
    }
}