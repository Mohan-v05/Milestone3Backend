using GYM_MILESTONETHREE.Models;
using GYM_MILESTONETHREE.Repository;
using GYM_MILESTONETHREE.RequestModels;

namespace GYM_MILESTONETHREE.Service
{
    public class SendmailService
    {
        private readonly SendMailRepository _sendMailRepository;
        private readonly EmailServiceProvider _emailServiceProvider;

        public SendmailService(SendMailRepository sendMailRepository, EmailServiceProvider emailServiceProvider)
        {
            _sendMailRepository = sendMailRepository ?? throw new ArgumentNullException(nameof(sendMailRepository));
            _emailServiceProvider = emailServiceProvider ?? throw new ArgumentNullException(nameof(emailServiceProvider));
        }

        public async Task<string> Sendmail(SendmailRequest sendMailRequest)
        {
            if (sendMailRequest == null) throw new ArgumentNullException(nameof(sendMailRequest));

            // Get email template
            var template = await _sendMailRepository.GetTemplate(sendMailRequest.EmailType).ConfigureAwait(false);
            if (template == null) throw new Exception("Template not found");

            // Generate email body based on the email type
            string bodyGenerated;
            if (sendMailRequest.EmailType == Enums.EmailTypes.otp)
            {
                bodyGenerated = await EmailBodyGenerate(template.Body, sendMailRequest.Name, sendMailRequest.Otp);
            }
            else if (sendMailRequest.EmailType == Enums.EmailTypes.PaymentNotification)
            {
                bodyGenerated = await EmailBodyGenerate(template.Body, sendMailRequest.Name, sendMailRequest.Amount);
            }
            else if (sendMailRequest.EmailType == Enums.EmailTypes.Deactive)
            {
                bodyGenerated = await EmailBodyGenerate2(template.Body, sendMailRequest.Name, sendMailRequest.Reason);
            }
            {
                throw new Exception("Unsupported email type");
            }

            // Create and send the email
            var mailModel = new MailModel
            {
                Subject = template.Title ?? string.Empty,
                Body = bodyGenerated,
                SenderName = "UnicomFitness",
                To = sendMailRequest.Email ?? throw new Exception("Recipient email address is required")
            };

            await _emailServiceProvider.SendMail(mailModel).ConfigureAwait(false);

            return "Email was sent successfully";
        }

        // Method to generate email body for OTP emails
        public Task<string> EmailBodyGenerate(string emailBody, string? name = null, string? otp = null)
        {
            var replacements = new Dictionary<string, string?>
            {
                { "{Name}", name },
                { "{Otp}", otp }
            };

            return Task.FromResult(ReplacePlaceholders(emailBody, replacements));
        }

        // Method to generate email body for PaymentNotification emails
        public Task<string> EmailBodyGenerate(string emailBody, string? name = null, decimal? amount = null)
        {
            var replacements = new Dictionary<string, string?>
            {
                { "{Name}", name },
                { "{Amount}", amount?.ToString("F2") }
            };

            return Task.FromResult(ReplacePlaceholders(emailBody, replacements));
        }
        //Methode to genwerate body for send deactivated message 
        public Task<string> EmailBodyGenerate2(string emailBody, string? name = null, string? reason = null)
        {
            var replacements = new Dictionary<string, string?>
            {
                { "{Name}", name },
                { "{Reason}", reason }
            };

            return Task.FromResult(ReplacePlaceholders(emailBody, replacements));
        }

        // Helper method to replace placeholders in email body
        private static string ReplacePlaceholders(string emailBody, Dictionary<string, string?> replacements)
        {
            foreach (var replacement in replacements)
            {
                if (!string.IsNullOrEmpty(replacement.Value))
                {
                    emailBody = emailBody.Replace(replacement.Key, replacement.Value, StringComparison.OrdinalIgnoreCase);
                }
            }
            return emailBody;
        }
    }
}
