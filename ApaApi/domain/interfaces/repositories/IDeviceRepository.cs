using domain.entities;
using System.Threading.Tasks;

namespace domain.interfaces.repositories
{
    public interface IDeviceRepository
    {
        Task<Device> FindByUserIdAsync(string userId);
        Task CreateAsync(Device device);
        Task UpdateTokenAsync(string token, int deviceId);
    }
}
