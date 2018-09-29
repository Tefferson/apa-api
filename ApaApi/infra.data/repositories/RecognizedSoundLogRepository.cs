using System;
using System.Threading.Tasks;
using domain.interfaces.repositories;
using domain.models;
using infra.data.context;
using Microsoft.EntityFrameworkCore;

namespace infra.data.repositories
{
    public class RecognizedSoundLogRepository : IRecognizedSoundLogRepository
    {
        private readonly ApaContext _context;

        public RecognizedSoundLogRepository(ApaContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(string sensorId, int labelNumber, double match)
        {
            var soundLog = new RecognizedSoundLog
            {
                LabelNumber = labelNumber,
                Match = match,
                SensorId = sensorId,
                CreationDate = DateTime.UtcNow
            };

            await _context.RecognizedSoundLog.AddAsync(soundLog);

            await _context.SaveChangesAsync();

            _context.Entry(soundLog).State = EntityState.Detached;
        }
    }
}
