using System;
using System.ComponentModel.DataAnnotations;

namespace domain.entities
{
    public class RecognizedSoundLog
    {
        [Key]
        public int Id { get; set; }
        public double Match { get; set; }
        public DateTime CreationDate { get; set; }

        public int LabelNumber { get; set; }

        public virtual Sensor Sensor { get; set; }
        public string SensorId { get; set; }

    }
}
