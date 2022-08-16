using AVG.Runtime.Element;
using Cysharp.Threading.Tasks;

namespace AVG.Runtime.Configuration
{
    public interface IConfigCenter
    {
        UniTask InitializeAsync(Config config);

        IElement InstanceConfig(Config config);
    }
}