using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace AVG.Runtime.Controller
{
    /// <summary>
    /// Responsible for managing all engine service processes.
    /// </summary>
    public static class EngineCore
    {
        //Subscribable engine events
        /// <summary>
        /// Invoked when engine initialization begins
        /// </summary>
        public static event Action OnInitializationBegins;
        /// <summary>
        /// Invoked when engine initialization ends
        /// </summary>
        public static event Action OnInitializationEnds;
        /// <summary>
        /// Invoked when the engine initialization progress is changed (in 0.0 to 1.0 range).
        /// </summary>
        public static event Action<float> OnInitializationProgress;
        /// <summary>
        /// Invoked when the engine is destroyed.
        /// </summary>
        public static event Action OnDestroyed;
        /// <summary>
        /// Proxy MonoBehaviour
        /// </summary>
        public static RuntimeBehavior runtimeBehavior { get; private set; }
        /// <summary>
        /// Whether the engine is initialized and ready
        /// </summary>
        public static bool initialized => _initializeTcs != null && _initializeTcs.Task.Status.IsCompleted();
        /// <summary>
        /// Whether the engine is currently being initialized
        /// </summary>
        public static bool initializing => _initializeTcs != null && !_initializeTcs.Task.Status.IsCompleted();

        private static UniTaskCompletionSource _initializeTcs;
        private static CancellationTokenSource _destroyCts;
        private static readonly List<Func<UniTask>> PreInitializationTasks = new List<Func<UniTask>>();
        private static readonly List<Func<UniTask>> PostInitializationTasks = new List<Func<UniTask>>();
        private static readonly List<IBasicService> Services = new List<IBasicService>();
        public static PlotPlayer.PlotPlayer Player;

        public static async UniTask InitializeAsync(RuntimeBehavior proxyBehavior,
            List<IBasicService> services)
        {
            //Make sure the engine is initialized only once
            if (initialized) return;
            if (initializing)
            {
                await _initializeTcs.Task;
                return;
            }

            //Construction asynchronous configuration
            _initializeTcs = new UniTaskCompletionSource();
            _destroyCts = new CancellationTokenSource();
            OnInitializationBegins?.Invoke();

            //Execute pre-initialization tasks
            for (var i = PreInitializationTasks.Count - 1; i >= 0; i--)
            {
                OnInitializationProgress?.Invoke(.25f * (1 - i / (float)PreInitializationTasks.Count));
                await PreInitializationTasks[i]();
                if (!initializing) return; // In case initialization process was terminated (eg: exited playmode).
            }

            //set mono behavior proxy
            runtimeBehavior = proxyBehavior;
            runtimeBehavior.OnMonoDestroyed += Destroy;

            //set services
            Services.Clear();
            Services.AddRange(services);

            for (var i = 0; i < Services.Count; i++)
            {
                OnInitializationProgress?.Invoke(.25f + .5f * (i / (float)Services.Count));
                await Services[i].InitializeAsync();
                if (!initializing) return;
            }
            //TODO：initialization

            //Execute post-initialization tasks
            for (var i = PostInitializationTasks.Count - 1; i >= 0; i--)
            {
                OnInitializationProgress?.Invoke(.75f + .25f * (1 - i / (float)PostInitializationTasks.Count));
                await PostInitializationTasks[i]();
                // In case initialization process was terminated (eg:exited playmode).
                if (!initialized) return;
            }


            _initializeTcs?.TrySetResult();
            OnInitializationEnds?.Invoke();
        }

        /// <summary>
        /// Uninstall all the engine services and stops the behaviour
        /// </summary>
        public static void Destroy()
        {
            _initializeTcs = null;
            //TODO:Uninstall all the engine services and stops the behaviour

            //remove runtime behavior
            if (runtimeBehavior != null)
            {
                runtimeBehavior.OnMonoDestroyed -= Destroy;
                runtimeBehavior.Destructor();
                runtimeBehavior = null;
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