using GYM_MILESTONETHREE.Enums;
using System.ComponentModel.DataAnnotations;

namespace GYM_MILESTONETHREE.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Email {  get; set; }

        public Role Role { get; set; }

        public string Nicnumber {  get; set; }

        public Address? Address { get; set; }

        public string Gender { get; set; }

        public string PasswordHashed { get; set; }

        public ICollection<Enrollments>? Enrollment { get; set; }

        public decimal? Fees { get; set; }

        public Boolean IsActivated { get; set; } = false ;

        public DateTime? ExpiryDate { get; set; }
    }
}
