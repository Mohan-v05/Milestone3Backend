using GYM_MILESTONETHREE.Enums;
using GYM_MILESTONETHREE.Models;

namespace GYM_MILESTONETHREE.RequestModels
{
    public class UpdateUser
    { 
        public string? Name { get; set; }

        public string? Email { get; set; }

        public Role Role { get; set; }

        public string? Nicnumber { get; set; }

        public Address? Address { get; set; }

        public string? Gender { get; set; }

      
    }
}
