using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

//TODO:Replace Task to UniTask
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
        /// Whether the engine is initialized and ready
        /// </summary>
        public static bool initialized => _initializeTcs != null && _initializeTcs.Task.IsCompleted;
        /// <summary>
        /// Whether the engine is currently being initialized
        /// </summary>
        public static bool initializing => _initializeTcs != null && !_initializeTcs.Task.IsCompleted;

        private static TaskCompletionSource<bool> _initializeTcs;
        private static CancellationTokenSource _destroyCts;
        private static readonly List<Func<Task>> PreInitializationTasks = new List<Func<Task>>();
        private static readonly List<Func<Task>> PostInitializationTasks = new List<Func<Task>>();

        public static async Task InitializeAsync()
        {
            //Make sure the engine is initialized only once
            if (initialized) return;
            if (initializing)
            {
                await _initializeTcs.Task;
                return;
            }

            //Initialize asynchronous configuration
            _initializeTcs = new TaskCompletionSource<bool>();
            _destroyCts = new CancellationTokenSource();

            //Execute pre-initialization tasks
            for (var i = PreInitializationTasks.Count - 1; i >= 0; i--)
            {
                OnInitializationProgress?.Invoke(.25f * (1 - i / (float)PreInitializationTasks.Count));
                await PreInitializationTasks[i]();
                if (!initializing) return; // In case initialization process was terminated (eg: exited playmode).
            }

            OnInitializationBegins?.Invoke();

            //TODO：initialization

            //Execute post-initialization tasks
            for (var i = PostInitializationTasks.Count - 1; i >= 0; i--)
            {
                OnInitializationProgress?.Invoke(.75f + .25f * (1 - i / (float)PostInitializationTasks.Count));
                await PostInitializationTasks[i]();
                if (!initialized) return; // In case initialization process was terminated (eg,:exited playmode).
            }


            _initializeTcs?.TrySetResult(new bool());
            OnInitializationEnds?.Invoke();
        }

        /// <summary>
        /// Uninstall all the engine services and stops the behaviour
        /// </summary>
        public static void Destroy()
        {
            _initializeTcs = null;
            //TODO:Uninstall all the engine services and stops the behaviour


            OnDestroyed?.Invoke();

            _destroyCts?.Cancel();
            _destroyCts?.Dispose();
            _destroyCts = null;
        }
    }
}