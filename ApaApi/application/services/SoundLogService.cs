using System.Collections.Generic;
using System.Threading.Tasks;
using application.interfaces.sound_log;
using domain.interfaces.repositories;
using domain.models;

namespace application.services
{
    public class SoundLogService : ISoundLogService
    {
        private readonly IRecognizedSoundLogRepository _soundLogRepository;

        public SoundLogService(IRecognizedSoundLogRepository soundLogRepository)
        {
            _soundLogRepository = soundLogRepository;
        }

        public Task<IEnumerable<SoundLogModel>> ListByUserAsync(string userId)
            => _soundLogRepository.ListByUserAsync(userId);
    }
}
