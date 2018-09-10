using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace domain.models
{
    public class Device
    {
        [Key]
        public int Id { get; set; }
        public string Token { get; set; }
        public virtual ICollection<SensorDevice> ObservedSensors { get; set; }
    }
}
