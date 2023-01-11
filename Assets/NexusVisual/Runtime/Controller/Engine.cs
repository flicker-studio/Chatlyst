using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace NexusVisual.Runtime
{
    /// <summary>
    /// Responsible for managing all service processes.
    /// </summary>
    public static class Engine
    {
        /// <summary>
        /// Invoked when the engine initialization progress is changed (in 0.0 to 1.0 range).
        /// </summary>
        public static event Action<float> OnInitializationProgress;
        /// <summary>
        /// Invoked when the engine is start.
        /// </summary>
        public static event Action OnStart;
        /// <summary>
        /// Invoked when the engine is destroyed.
        /// </summary>
        public static event Action OnDestroyed;
        /// <summary>
        /// Proxy MonoBehaviour
        /// </summary>
        private static RuntimeBehavior RuntimeBehavior { get; set; }
        /// <summary>
        /// Whether the engine is initialized and ready
        /// </summary>
        public static bool Initialized => _initializeTcs != null && _initializeTcs.Task.Status.IsCompleted();
        /// <summary>
        /// Whether the engine is currently being initialized
        /// </summary>
        public static bool Initializing => _initializeTcs != null && !_initializeTcs.Task.Status.IsCompleted();

        private static UniTaskCompletionSource _initializeTcs;
        private static CancellationTokenSource _destroyCts;
        private static readonly List<IBasicService> ServicesList = new List<IBasicService>();
        private static readonly Dictionary<Type, IBasicService> ServicesCache = new Dictionary<Type, IBasicService>();
        //public static PlotPlayer player;

        public static async UniTask InitializeAsync(RuntimeBehavior proxyBehavior,
            List<IBasicService> services)
        {
            //Make sure the engine is initialized only once
            if (Initialized) return;
            if (Initializing)
            {
                await _initializeTcs.Task;
                return;
            }

            //Construction asynchronous configuration
            _initializeTcs = new UniTaskCompletionSource();
            _destroyCts = new CancellationTokenSource();
            //OnInitializationBegins?.Invoke();
/*
            //Execute pre-initialization tasks
            for (var i = PreInitializationTasks.Count - 1; i >= 0; i--)
            {
                OnInitializationProgress?.Invoke(.25f * (1 - i / (float)PreInitializationTasks.Count));
                await PreInitializationTasks[i]();
                if (!Initializing) return; // In case initialization process was terminated (eg: exited playmode).
            }
*/

            #region Monobehavior proxy

            RuntimeBehavior = proxyBehavior;
            RuntimeBehavior.OnMonoStart += OnStart;
            RuntimeBehavior.OnMonoDestroyed += Destroy;

            #endregion


            # region services initialize

            ServicesList.Clear();
            for (var i = 0; i < services.Count; i++)
            {
                OnInitializationProgress?.Invoke(.25f + .5f * (i / (float)ServicesList.Count));
                await services[i].InitializeAsync();
                ServicesList.Add(services[i]);
                if (!Initializing) return;
            }

            #endregion

/*
            //Execute post-initialization tasks
            for (var i = PostInitializationTasks.Count - 1; i >= 0; i--)
            {
                OnInitializationProgress?.Invoke(.75f + .25f * (1 - i / (float)PostInitializationTasks.Count));
                await PostInitializationTasks[i]();
                // In case initialization process was terminated (eg:exited playmode).
                if (!Initialized) return;
            }

*/
            _initializeTcs?.TrySetResult();
            //  OnInitializationEnds?.Invoke();
        }

        [CanBeNull]
        public static IBasicService TryGetService<T>()
        {
            if (ServicesCache.TryGetValue(typeof(T), out var resultService)) return resultService;
            resultService = ServicesList.FirstOrDefault(typeof(T).IsInstanceOfType);
            if (resultService == null) return null;
            ServicesCache.Add(typeof(T), resultService);
            return resultService;
        }

        /// <summary>
        /// Uninstall all the engine services and stops the behaviour
        /// </summary>
        private static void Destroy()
        {
            _initializeTcs = null;
            //destroy services
            foreach (var service in ServicesList) service.Destroy();

            //clean cache
            ServicesList.Clear();
            ServicesCache.Clear();

            //remove runtime behavior
            if (RuntimeBehavior != null)
            {
                RuntimeBehavior.OnMonoDestroyed -= Destroy;
                RuntimeBehavior.Destructor();
                RuntimeBehavior = null;
            }

            //Invoke destroy event
            OnDestroyed?.Invoke();

            //stop all tasks
            _destroyCts?.Cancel();
            _destroyCts?.Dispose();
            _destroyCts = null;
        }
    }
}