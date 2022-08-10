using System.Threading.Tasks;

namespace AVG.Runtime.Controller
{
    /// <summary>
    ///provide basic services
    /// </summary>
    public interface IBasicService
    {
        Task InitializeAsync();
        void Destroy();
    }
}