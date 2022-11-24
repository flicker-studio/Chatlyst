using Cysharp.Threading.Tasks;

namespace NexusVisual.Runtime
{
    /// <summary>
    ///provide basic services
    /// </summary>
    public interface IBasicService
    {
        public UniTask InitializeAsync();
        public void Destroy();
    }
}