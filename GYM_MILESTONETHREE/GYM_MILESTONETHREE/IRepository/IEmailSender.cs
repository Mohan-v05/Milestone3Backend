namespace GYM_MILESTONETHREE.IRepository
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(string toEmail, string subject, string body);
    }
}
