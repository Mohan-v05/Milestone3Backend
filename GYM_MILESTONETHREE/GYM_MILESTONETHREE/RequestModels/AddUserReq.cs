using GYM_MILESTONETHREE.Enums;
using GYM_MILESTONETHREE.Models;
using System.ComponentModel.DataAnnotations;

namespace GYM_MILESTONETHREE.RequestModels
{
    public class AddUserReq
    {
        [Required]
        public string Name { get; set; }

        public string email { get; set; }

        public String gender {  get; set; }
        public Role Role { get; set; }

        public string Nicnumber { get; set; }

        public AddressReq? Address { get; set; }

        public string Password { get; set; }

        public Boolean isActivated { get; set; }
    }
}
