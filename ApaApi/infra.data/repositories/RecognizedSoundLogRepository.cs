using System;
using System.Threading.Tasks;
using domain.interfaces.repositories;
using domain.entities;
using infra.data.context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using domain.models;
using System.Collections.Generic;

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

            _context.Attach(soundLog);
            _context.Entry(soundLog).State = EntityState.Added;
            await _context.SaveChangesAsync();
            _context.Entry(soundLog).State = EntityState.Detached;
        }

        public async Task<IEnumerable<SoundLogModel>> ListByUserAsync(string userId)
        {
            var events = await _context
                            .RecognizedSoundLog
                            .Include(r => r.Sensor)
                            .Where(e => e.Sensor.ObservingDevices.Any(o => o.Device.UserId == userId))
                            .Select(r => new
                            {
                                r.CreationDate,
                                r.Sensor.PlaceAlias,
                                r.Sensor.RoomTag,
                                r.LabelNumber
                            })
                            .AsNoTracking()
                            .ToListAsync();

            var labelNumbers = events.Select(e => e.LabelNumber).Distinct();

            var labels = _context
                .SoundLabel
                .AsNoTracking()
                .Where(l => labelNumbers.Contains(l.LabelNumber));

            return events.Select(e => new SoundLogModel
            {
                CreationDate = e.CreationDate,
                PlaceAlias = e.PlaceAlias,
                RoomTag = e.RoomTag,
                Label = labels.First(l => l.LabelNumber == e.LabelNumber).LabelDescription
            });
        }
    }
}
