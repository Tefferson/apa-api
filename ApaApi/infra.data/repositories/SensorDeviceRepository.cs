using System.Threading.Tasks;
using domain.interfaces.repositories;
using domain.entities;
using infra.data.context;
using Microsoft.EntityFrameworkCore;

namespace infra.data.repositories
{
    public class SensorDeviceRepository : ISensorDeviceRepository
    {
        private readonly ApaContext _context;

        public SensorDeviceRepository(ApaContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(string sensorId, int deviceId)
        {
            var sensorDevice = new SensorDevice
            {
                DeviceId = deviceId,
                SensorId = sensorId
            };

            _context.Attach(sensorDevice);
            _context.Entry(sensorDevice).State = EntityState.Added;
            await _context.SaveChangesAsync();
            _context.Entry(sensorDevice).State = EntityState.Detached;
        }

        public async Task DeleteAsync(SensorDevice sensorDevice)
        {
            _context.Attach(sensorDevice);
            _context.Entry(sensorDevice).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            _context.Entry(sensorDevice).State = EntityState.Detached;
        }
    }
}
