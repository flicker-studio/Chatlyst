using System.Threading.Tasks;
using UnityEngine;

namespace AVG.Runtime.Controller
{
    public class RuntimeInitializer : MonoBehaviour
    {
        private static TaskCompletionSource<bool> _initializeTcs;

        public static async Task InitializeAsync()
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