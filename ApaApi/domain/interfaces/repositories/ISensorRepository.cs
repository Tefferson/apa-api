using domain.models;
using System.Threading.Tasks;

namespace domain.interfaces.repositories
{
    public interface ISensorRepository
    {
        Task<Sensor> FindAsync(string id);
    }
}
