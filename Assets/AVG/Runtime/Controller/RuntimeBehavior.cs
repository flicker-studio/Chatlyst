using System;
using UnityEngine;

namespace AVG.Runtime.Controller
{
    public class RuntimeBehavior : MonoBehaviour
    {
        public event Action OnStart;
        public event Action OnUpdate;
        public event Action OnDestroyed;

        private GameObject m_BehaviourGameObject;
        private RuntimeBehavior m_RuntimeBehavior;

        /// <summary>
        /// Initialize root GameObject
        /// </summary>
        public static RuntimeBehavior Initialize()
        {
            var rb = new GameObject("[RuntimeBehavior]");
            DontDestroyOnLoad(rb);
            var runtimeBehaviorComponent = rb.AddComponent<RuntimeBehavior>();
            runtimeBehaviorComponent.m_BehaviourGameObject = rb;
            runtimeBehaviorComponent.m_RuntimeBehavior = runtimeBehaviorComponent;
            return runtimeBehaviorComponent;
        }

        public GameObject GetRoot() => m_BehaviourGameObject;

        public void SetRootGameObject(GameObject child)
        {
            child.transform.SetParent(transform);
        }

        private void Start()
        {
            OnStart?.Invoke();
        }

        private void Update()
        {
            OnUpdate?.Invoke();
        }

        private void OnDestroy()
        {
            OnDestroyed?.Invoke();
        }
    }
}