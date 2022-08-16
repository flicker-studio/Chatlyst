using Cysharp.Threading.Tasks;
using UnityEngine;

namespace AVG.Runtime.Controller
{
    public class RuntimeInitializer : MonoBehaviour
    {
        private static UniTaskCompletionSource _initializeTcs;

        public static async UniTask InitializeAsync()
        {
            if (EngineCore.initialized) return;
            if (_initializeTcs != null)
            {
                await _initializeTcs.Task;
                return;
            }

            await EngineCore.InitializeAsync();
        }
    }
}