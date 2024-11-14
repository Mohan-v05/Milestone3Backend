using System.Net.Mail;
using System.Net;
using GYM_MILESTONETHREE.IRepository;

namespace GYM_MILESTONETHREE.Repository
{
    public class EmailSender : IEmailSender
    {
        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                var fromAddress = new MailAddress("your-email@example.com", "Your Gym Name");
                var toAddress = new MailAddress(toEmail);
                const string fromPassword = "your-email-password";  // Use a secure method to store passwords

                var smtp = new SmtpClient
                {
                    Host = "smtp.example.com",  // Replace with your SMTP provider
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    await smtp.SendMailAsync(message);
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log or handle email sending failures
                return false;
            }
        }
    }
}
