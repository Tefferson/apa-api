using domain.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace application.interfaces.sound_log
{
    public interface ISoundLogService
    {
        Task<IEnumerable<SoundLogModel>> ListByUserAsync(string userId);
    }
}
