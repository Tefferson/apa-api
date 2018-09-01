using application.models.sensor_information;
using System.Threading.Tasks;

namespace application.interfaces.sensor_information
{
    public interface ISensorInformationService
    {
        Task<SensorInformationModel> GetInformationAsync(string sensorId);
    }
}
