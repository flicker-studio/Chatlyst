using System.Threading.Tasks;
using AVG.Runtime.Element;

namespace AVG.Runtime.Configuration
{
    public interface IConfigCenter
    {
        Task InitializeAsync(Config config);

        IElement InstanceConfig(Config config);
    }
}