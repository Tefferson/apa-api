using System.Collections.Generic;
using System.Threading.Tasks;
using application.interfaces.sensor_information;
using application.models.sensor_information;
using domain.constants;

namespace application.services
{
    public class SensorInformationService : ISensorInformationService
    {
        public async Task<SensorInformationModel> GetInformationAsync(string sensorId)
        {
            await Task.CompletedTask;
            return new SensorInformationModel
            {
                LocationAlias = "[Casa] Cozinha",
                RoomTag = Room.Kitchen,
                ObservingDevices = new List<DeviceInformation> {
                        new DeviceInformation
                        {
                            Token = "d05T7WTY_mw:APA91bEvULvwYkb04yUEQHFjmsmum-smg5FaGvErbPYQTu6N2BzzV9zW9OqefTcNZ5bQsGf18qKZdCd5U0hJggoMU8c748wwbKZWmtkxTQNUyI82VRx9OZs_xvOs_jTNvsvqQRh5PDXu"
                        }
                    }
            };
        }
    }
}
