namespace GYM_MILESTONETHREE.RequestModels
{
    public class UpdatePasswordReq
    {
        public int id { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
        public string nic {  get; set; }
    }
}
