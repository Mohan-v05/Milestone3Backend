using GYM_MILESTONETHREE.Models;
using GYM_MILESTONETHREE.Repository;
using GYM_MILESTONETHREE.RequestModels;

namespace GYM_MILESTONETHREE.Service
{
    public class SendmailService(SendMailRepository _sendMailRepository, EmailServiceProvider _emailServiceProvider)
    {
        public async Task<string> Sendmail(SendmailRequest sendMailRequest)
        {
            if (sendMailRequest == null) throw new ArgumentNullException(nameof(sendMailRequest));

            var template = await _sendMailRepository.GetTemplate(sendMailRequest.EmailType).ConfigureAwait(false);
            if (template == null) throw new Exception("Template not found");

            var bodyGenerated = await EmailBodyGenerate(template.Body, sendMailRequest.Name, sendMailRequest.Otp);

            MailModel mailModel = new MailModel
            {
                Subject = template.Title ?? string.Empty,
                Body = bodyGenerated ?? string.Empty,
                SenderName = "UnicomFiteness",
                To = sendMailRequest.Email ?? throw new Exception("Recipient email address is required")
            };

            await _emailServiceProvider.SendMail(mailModel).ConfigureAwait(false);

            return "email was sent successfully";
        }

        public async Task<string> EmailBodyGenerate(string emailbody, string? name = null, string? otp = null)
        {
            var replacements = new Dictionary<string, string?>
            {
                { "{Name}", name },
                { "{Otp}", otp }
            };

            foreach (var replacement in replacements)
            {
                if (!string.IsNullOrEmpty(replacement.Value))
                {
                    emailbody = emailbody.Replace(replacement.Key, replacement.Value, StringComparison.OrdinalIgnoreCase);
                }
            }

            return emailbody;
        }

    }
}

