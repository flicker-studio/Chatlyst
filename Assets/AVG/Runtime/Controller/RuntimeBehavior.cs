using System;
using AVG.Runtime.Element.View;
using UnityEngine;

namespace AVG.Runtime.Controller
{
    public class RuntimeBehavior : MonoBehaviour
    {
        public event Action OnAwake;
        public event Action OnStart;
        public event Action OnUpdate;
        public event Action OnDestroyed;

        public RuntimeBehavior behavior;
        private ViewManager _viewManager;

        private void Awake()
        {
            var vm = new GameObject("RuntimeBehavior");
            DontDestroyOnLoad(vm);
            behavior = this;
            _viewManager = new ViewManager();
            _viewManager.InitializeAsync();
            _viewManager.ve.ChangePositionAsync(new Vector3(0, 0, 100), 100);
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