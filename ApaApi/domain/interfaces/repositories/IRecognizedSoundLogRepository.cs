﻿using domain.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace domain.interfaces.repositories
{
    public interface IRecognizedSoundLogRepository
    {
        Task CreateAsync(string sensorId, int labelNumber, double match);
        Task<IEnumerable<SoundLogModel>> ListByUserAsync(string userId);
    }
}
