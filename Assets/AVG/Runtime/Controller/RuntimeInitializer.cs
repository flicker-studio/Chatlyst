using System.Collections.Generic;
using AVG.Runtime.Configuration;
using AVG.Runtime.Element.View;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AVG.Runtime.Controller
{
    public class RuntimeInitializer : MonoBehaviour
    {
        private static UniTaskCompletionSource _initializeTcs;
        [SerializeField]
        private bool initOnAwake = true;

        /// <summary>
        /// AVG Framework entrance
        /// </summary>
        /// <param name="configDistribution">engine configuration file</param>
        public static async UniTask InitializeAsync(IConfigDistribution configDistribution = null)
        {
            if (EngineCore.initialized) return;
            if (_initializeTcs != null)
            {
                await _initializeTcs.Task;
                return;
            }

            _initializeTcs = new UniTaskCompletionSource();
            configDistribution ??= new ConfigDistribution();

            var runtimeBehavior = RuntimeBehavior.Construction();
            var manager = new List<IBasicService>
            {
                new ViewManager(), new PlotPlayer.PlotPlayer()
            };

            await EngineCore.InitializeAsync(runtimeBehavior, manager);
            // In case terminated in the midst of initialization.
            if (!EngineCore.initialized)
            {
                DisposeTcs();
                return;
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void DisposeTcs()
        {
            _initializeTcs?.TrySetResult();
            _initializeTcs = null;
        }

        private void Awake()
        {
            if (initOnAwake)
            {
                InitializeAsync().Forget();
            }
        }
    }
}