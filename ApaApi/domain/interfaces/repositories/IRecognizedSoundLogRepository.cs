using System.Threading.Tasks;

namespace domain.interfaces.repositories
{
    public interface IRecognizedSoundLogRepository
    {
        Task CreateAsync(string sensorId, int labelNumber, double match);
    }
}
