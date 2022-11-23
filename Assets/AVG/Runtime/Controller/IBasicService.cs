using Cysharp.Threading.Tasks;

namespace AVG.Runtime
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