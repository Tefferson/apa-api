using System.ComponentModel.DataAnnotations;

namespace domain.entities
{
    public class SoundLabel
    {
        [Key]
        public int Id { get; set; }
        public string LabelDescription { get; set; }
        public int LabelNumber { get; set; }
    }
}
