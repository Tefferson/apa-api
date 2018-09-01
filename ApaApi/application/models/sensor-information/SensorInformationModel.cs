using System.Collections.Generic;

namespace application.models.sensor_information
{
    public class SensorInformationModel
    {
        public string LocationAlias { get; set; }
        public string RoomTag { get; set; }
        public IEnumerable<DeviceInformation> ObservingDevices { get; set; }
    }

    public class DeviceInformation
    {
        public string Token { get; set; }
    }
}
