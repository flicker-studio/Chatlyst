using AVG.Runtime.Configuration;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AVG.Runtime.Controller
{
    public class RuntimeInitializer : MonoBehaviour
    {
        private static UniTaskCompletionSource _initializeTcs;

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

#if UNITY_EDITOR
            Debug.Log("InitializeAsync Start");
#endif
            _initializeTcs = new UniTaskCompletionSource();
            configDistribution ??= new ConfigDistribution();

            var behaviour = RuntimeBehavior.Initialize();


            await EngineCore.InitializeAsync();
            // In case terminated in the midst of initialization.
            if (!EngineCore.initialized)
            {
                DisposeTcs();
                return;
            }

#if UNITY_EDITOR
            Debug.Log("InitializeAsync End");
#endif
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void DisposeTcs()
        {
            _initializeTcs?.TrySetResult();
            _initializeTcs = null;
#if UNITY_EDITOR
            Debug.Log("InitializeAsync Stop");
#endif
        }
    }
}