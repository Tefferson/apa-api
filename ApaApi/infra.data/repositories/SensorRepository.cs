using domain.interfaces.repositories;
using domain.entities;
using infra.data.context;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace infra.data.repositories
{
    public class SensorRepository : ISensorRepository
    {
        private readonly ApaContext _context;

        public SensorRepository(ApaContext context)
        {
            _context = context;
        }

        public Task<Sensor> FindAsync(string id) =>
            _context.Sensor
                .AsNoTracking()
                .Include(s => s.ObservingDevices)
                .ThenInclude(o => o.Device)
                .FirstOrDefaultAsync(s => s.Id == id);
    }
}
