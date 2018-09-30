using System.Threading.Tasks;
using domain.entities;

namespace domain.interfaces.repositories
{
    public interface ISensorDeviceRepository
    {
        Task CreateAsync(string sensorId, int deviceId);
        Task DeleteAsync(SensorDevice sensorDevice);
    }
}
