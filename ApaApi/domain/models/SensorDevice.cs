using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace domain.models
{
    public class SensorDevice
    {
        [Key]
        public int Id { get; set; }

        public virtual Sensor Sensor { get; set; }
        public string SensorId { get; set; }

        public virtual Device Device { get; set; }
        public int DeviceId { get; set; }
    }
}
