using System;
using UnityEngine;

namespace AVG.Runtime
{
    public class RuntimeBehavior : MonoBehaviour
    {
        public event Action OnMonoStart;
        public event Action OnMonoUpdate;
        public event Action OnMonoDestroyed;


        /// <summary>
        /// Construction root GameObject
        /// </summary>
        public static RuntimeBehavior Construction()
        {
            var rb = new GameObject("[RuntimeBehavior]");
            DontDestroyOnLoad(rb);
            var runtimeBehaviorComponent = rb.AddComponent<RuntimeBehavior>();
            return runtimeBehaviorComponent;
        }

        public void Destructor() =>
            DestroyImmediate(gameObject);

        //Unity event function
        private void Start() =>
            OnMonoStart?.Invoke();


        private void Update() =>
            OnMonoUpdate?.Invoke();


        private void OnDestroy() =>
            OnMonoDestroyed?.Invoke();
    }
}