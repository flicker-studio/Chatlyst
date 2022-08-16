using Cysharp.Threading.Tasks;

namespace AVG.Runtime.Controller
{
    /// <summary>
    ///provide basic services
    /// </summary>
    public interface IBasicService
    {
        UniTask InitializeAsync();
        void Destroy();
    }
}