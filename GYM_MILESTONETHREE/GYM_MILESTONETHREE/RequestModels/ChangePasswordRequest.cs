namespace GYM_MILESTONETHREE.RequestModels
{
    public class ChangePasswordRequest
    {
        public string Nic { get; set; }
        public int Id { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
