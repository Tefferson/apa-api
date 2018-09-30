using System.Threading.Tasks;

namespace application.interfaces.device
{
    public interface IDeviceService
    {
        Task AddOrUpdateToken(string token, string userId);
    }
}
