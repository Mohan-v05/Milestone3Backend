using System.ComponentModel.DataAnnotations;

namespace GYM_MILESTONETHREE.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        public string firstLine { get; set; }

        public string secondLine { get; set; }

        public string city { get; set; }

        public Users? user { get; set; }

        public int? userId { get; set; }
    }
}
