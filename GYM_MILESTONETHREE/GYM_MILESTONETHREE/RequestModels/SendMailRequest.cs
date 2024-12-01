using GYM_MILESTONETHREE.Enums;

namespace GYM_MILESTONETHREE.RequestModels
{
    public class SendmailRequest
    {
        public string? Name { get; set; }
        public string? Otp { get; set; }

        public string? Email { get; set; }
        public EmailTypes EmailType { get; set; }
    }
}
