using System;
using UnityEngine;

namespace AVG.Runtime.Controller
{
    public class RuntimeBehavior : MonoBehaviour
    {
        public event Action OnMonoStart;
        public event Action OnMonoUpdate;
        public event Action OnMonoDestroyed;

        private GameObject m_BehaviourGameObject;
        private RuntimeBehavior m_RuntimeBehavior;

        /// <summary>
        /// Construction root GameObject
        /// </summary>
        public static RuntimeBehavior Construction()
        {
            var rb = new GameObject("[RuntimeBehavior]");
            DontDestroyOnLoad(rb);
            var runtimeBehaviorComponent = rb.AddComponent<RuntimeBehavior>();
            runtimeBehaviorComponent.m_BehaviourGameObject = rb;
            runtimeBehaviorComponent.m_RuntimeBehavior = runtimeBehaviorComponent;
            return runtimeBehaviorComponent;
        }

        public void Destructor()
        {
            if (m_RuntimeBehavior && m_RuntimeBehavior.gameObject)
                Destroy(m_RuntimeBehavior.gameObject);
        }

        public void SetRootGameObject(GameObject child)
        {
            child.transform.SetParent(transform);
        }


        //Unity event function
        private void Start()
        {
            OnMonoStart?.Invoke();
        }

        private void Update()
        {
            OnMonoUpdate?.Invoke();
        }

        private void OnDestroy()
        {
            OnMonoDestroyed?.Invoke();
        }
    }
}