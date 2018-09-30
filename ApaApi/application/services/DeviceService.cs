using System.Threading.Tasks;
using application.interfaces.device;
using domain.interfaces.repositories;
using domain.entities;

namespace application.services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;

        public DeviceService(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public async Task AddOrUpdateToken(string token, string userId)
        {
            var device = await _deviceRepository.FindByUserIdAsync(userId);
            if (device == null)
                await _deviceRepository.CreateAsync(new Device
                {
                    Token = token,
                    UserId = userId
                });
            else if (device.Token != token)
                await _deviceRepository.UpdateTokenAsync(token, device.Id);
        }
    }
}
