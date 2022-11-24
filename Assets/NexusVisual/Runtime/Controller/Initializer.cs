using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using NexusVisual.Runtime.Configuration;
using UnityEngine;

namespace NexusVisual.Runtime
{
    public class Initializer : MonoBehaviour
    {
        private static UniTaskCompletionSource _initializeTcs;
        [SerializeField]
        private bool initOnAwake = true;

        private void Awake()
        {
            if (initOnAwake) InitializeAsync().Forget();
        }

        /// <summary>
        /// AVG Framework entrance
        /// </summary>
        /// <param name="configDistribution">engine configuration file</param>
        private static async UniTask InitializeAsync(IConfigDistribution configDistribution = null)
        {
            if (Engine.Initialized) return;
            if (_initializeTcs != null)
            {
                await _initializeTcs.Task;
                return;
            }

            _initializeTcs = new UniTaskCompletionSource();

            var runtimeBehavior = RuntimeBehavior.Construction();
            var manager = new List<IBasicService>
            {
                new ViewManager(),
                new PlotPlayer()
            };

            await Engine.InitializeAsync(runtimeBehavior, manager);
            // In case terminated in the midst of initialization.
            if (!Engine.Initialized) DisposeTcs();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void DisposeTcs()
        {
            _initializeTcs?.TrySetResult();
            _initializeTcs = null;
        }
    }
}