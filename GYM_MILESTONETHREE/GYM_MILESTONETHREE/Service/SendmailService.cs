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
            if (string.IsNullOrEmpty(sendMailRequest.Email)) throw new ArgumentNullException(nameof(sendMailRequest.Email));

            // Get email template
            var template = await _sendMailRepository.GetTemplate(sendMailRequest.EmailType).ConfigureAwait(false);
            if (template == null) throw new Exception("Template not found");

            // Generate email body based on the email type
            string bodyGenerated;
            if (sendMailRequest.EmailType == Enums.EmailTypes.otp)
            {
                bodyGenerated = await EmailBodyGenerateForOtp(template.Body, sendMailRequest.Name, sendMailRequest.Email, sendMailRequest.Password).ConfigureAwait(false);
            }
            else if (sendMailRequest.EmailType == Enums.EmailTypes.PaymentNotification)
            {
                bodyGenerated = await EmailBodyGenerateForPaymentNotification(template.Body, sendMailRequest.Name, sendMailRequest.Amount).ConfigureAwait(false);
            }
            else if (sendMailRequest.EmailType == Enums.EmailTypes.Deactive)
            {
                bodyGenerated = await EmailBodyGenerateForDeactivation(template.Body, sendMailRequest.Name, sendMailRequest.Reason).ConfigureAwait(false);
            }
            else
            {
                throw new Exception("Unsupported email type");
            }

            // Create the mail model
            var mailModel = new MailModel
            {
                Subject = template.Title ?? string.Empty,
                Body = bodyGenerated,
                SenderName = "UnicomFitness",
                To = sendMailRequest.Email
            };

            // Send the email asynchronously
            await _emailServiceProvider.SendMail(mailModel).ConfigureAwait(false);

            return "Email was sent successfully";
        }

        // Method to generate email body for OTP emails
        public async Task<string> EmailBodyGenerateForOtp(string emailBody, string? name = null, string? username = null,string? Password =null )
        {
            var replacements = new Dictionary<string, string?>
            {
                { "{Name}", name },
                { "{Username}", username },
                { "{Password}", Password}
            };

            return await Task.FromResult(ReplacePlaceholders(emailBody, replacements)).ConfigureAwait(false);
        }

        // Method to generate email body for PaymentNotification emails
        public async Task<string> EmailBodyGenerateForPaymentNotification(string emailBody, string? name = null, string? amount = null)
        {
            var replacements = new Dictionary<string, string?>
            {
                { "{Name}", name },
                { "{Amount}", amount }
            };

            return await Task.FromResult(ReplacePlaceholders(emailBody, replacements)).ConfigureAwait(false);
        }

        // Method to generate email body for Deactivation emails
        public async Task<string> EmailBodyGenerateForDeactivation(string emailBody, string? name = null, string? reason = null)
        {
            var replacements = new Dictionary<string, string?>
            {
                { "{Name}", name },
                { "{Reason}", reason }
            };

            return await Task.FromResult(ReplacePlaceholders(emailBody, replacements)).ConfigureAwait(false);
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
