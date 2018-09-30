using System.Threading.Tasks;
using domain.interfaces.repositories;
using domain.entities;
using infra.data.context;
using Microsoft.EntityFrameworkCore;

namespace infra.data.repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly ApaContext _context;

        public DeviceRepository(ApaContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Device device)
        {
            _context.Attach(device);
            _context.Entry(device).State = EntityState.Added;
            await _context.SaveChangesAsync();
            _context.Entry(device).State = EntityState.Detached;
        }

        public Task<Device> FindByUserIdAsync(string userId)
            => _context.Device.AsNoTracking().FirstOrDefaultAsync(d => d.UserId == userId);

        public async Task UpdateTokenAsync(string token, int deviceId)
        {
            var device = new Device
            {
                Id = deviceId,
                Token = token
            };

            _context.Attach(device);
            _context.Entry(device).Property(d => d.Token).IsModified = true;
            await _context.SaveChangesAsync();
            _context.Entry(device).State = EntityState.Detached;
        }
    }
}
