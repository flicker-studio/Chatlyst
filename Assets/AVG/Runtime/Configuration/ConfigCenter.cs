using System.Threading.Tasks;
using AVG.Runtime.Element;

namespace AVG.Runtime.Configuration
{
    public class ConfigCenter : IConfigCenter
    {
        public async Task InitializeAsync(Config config)
        {
            throw new System.NotImplementedException();
        }

        public IElement InstanceConfig(Config config)
        {
            throw new System.NotImplementedException();
        }
    }
}