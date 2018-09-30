using System.Threading.Tasks;

namespace application.interfaces.sensor
{
    public interface ISensorService
    {
        Task Subscribe(string sensorId, string userId);
        Task Unsubscribe(string sensorId, string userId);
    }
}
