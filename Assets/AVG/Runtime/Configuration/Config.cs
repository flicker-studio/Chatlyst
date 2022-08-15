using UnityEngine;

namespace AVG.Runtime.Configuration
{
    public abstract class Config : ScriptableObject

    {
        public ElementType type;
        public Vector3 position;
        public Quaternion rotation;
    }
}