using AVG.Runtime.Element;
using Cysharp.Threading.Tasks;

namespace AVG.Runtime.Configuration
{
    public class ConfigCenter : IConfigCenter
    {
        public async UniTask InitializeAsync(Config config)
        {
            throw new System.NotImplementedException();
        }

        public IElement InstanceConfig(Config config)
        {
            throw new System.NotImplementedException();
        }
    }
}