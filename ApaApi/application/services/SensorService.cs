using System.Linq;
using System.Threading.Tasks;
using application.interfaces.sensor;
using domain.interfaces.repositories;

namespace application.services
{
    public class SensorService : ISensorService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly ISensorRepository _sensorRepository;
        private readonly ISensorDeviceRepository _sensorDeviceRepository;

        public SensorService(
            IDeviceRepository deviceRepository,
            ISensorRepository sensorRepository,
            ISensorDeviceRepository sensorDeviceRepository
        )
        {
            _deviceRepository = deviceRepository;
            _sensorRepository = sensorRepository;
            _sensorDeviceRepository = sensorDeviceRepository;
        }

        public async Task Subscribe(string sensorId, string userId)
        {
            var sensor = await _sensorRepository.FindAsync(sensorId);
            if (sensor == null) return;

            var device = await _deviceRepository.FindByUserIdAsync(userId);
            if (device == null) return;

            if (sensor.ObservingDevices.Any(o => o.DeviceId == device.Id)) return;

            await _sensorDeviceRepository.CreateAsync(sensorId, device.Id);
        }

        public async Task Unsubscribe(string sensorId, string userId)
        {
            var sensor = await _sensorRepository.FindAsync(sensorId);
            if (sensor == null) return;

            var device = await _deviceRepository.FindByUserIdAsync(userId);
            if (device == null) return;

            var sensorDevice = sensor.ObservingDevices.FirstOrDefault(o => o.DeviceId == device.Id);
            if (sensorDevice == null) return;
                       
            await _sensorDeviceRepository.DeleteAsync(sensorDevice);
        }
    }
}
