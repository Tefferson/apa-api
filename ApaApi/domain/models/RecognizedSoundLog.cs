using System;
using System.ComponentModel.DataAnnotations;

namespace domain.models
{
    public class RecognizedSoundLog
    {
        public double Match { get; set; }

        public DateTime CreationDate { get; set; }

        public int LabelNumber { get; set; }

        public virtual Sensor Sensor { get; set; }
        public string SensorId { get; set; }

    }
}
