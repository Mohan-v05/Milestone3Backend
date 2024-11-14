using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GYM_MILESTONETHREE.Models
{
    public class GymPrograms
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        public string? Category { get; set; }

        [Required]
        public decimal Fees { get; set; }

        public string? ImagePath { get; set; }

        public ICollection<Enrollments>? enrollments { get; set; }
    }
}
