using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace domain.models
{
    public class Sensor
    {
        [Key]
        public string Id { get; set; }
        public string PlaceAlias { get; set; }
        public string RoomTag { get; set; }
        public virtual ICollection<SensorDevice> ObservingDevices { get; set; }
    }
}
