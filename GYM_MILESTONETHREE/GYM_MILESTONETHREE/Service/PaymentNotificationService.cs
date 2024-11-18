//using GYM_MILESTONETHREE.Models;
//using System.Net.Mail;
//using System.Net;
//using GYM_MILESTONETHREE.DataBase;
//using GYM_MILESTONETHREE.IRepository;
//using Microsoft.EntityFrameworkCore;

//namespace GYM_MILESTONETHREE.Service
//{
//    public class PaymentNotificationService
//    {
//            private readonly AppDb _context;
//            private readonly ILogger<PaymentNotificationService> _logger;
//            private readonly IEmailSender _emailSender; // Assuming you have an email service

//            public PaymentNotificationService(AppDb context, ILogger<PaymentNotificationService> logger, IEmailSender emailSender)
//            {
//                _context = context;
//                _logger = logger;
//                _emailSender = emailSender;
//            }

//            // Method to check for payments expiring in 1 day
//            public async Task SendExpirationNotificationsAsync()
//            {
//                // Get payments that expire tomorrow (i.e., expiration date is tomorrow)
//                var paymentsExpiringTomorrow = await _context.payments
//                    .Where(p => p.ExpiryDate.HasValue && p.ExpiryDate.Value.Date == DateTime.UtcNow.Date.AddDays(30))
//                    .Include(p => p.User)  // Include User data to send the email
//                    .ToListAsync();

//                foreach (var payment in paymentsExpiringTomorrow)
//                {
//                    var user = payment.User;

//                    if (user != null && user.IsActivated)
//                    {
//                        // Create notification message
//                        var message = $"Dear {user.Name}, your payment for gym membership is set to expire on {payment.ExpiryDate.Value.ToShortDateString()}. Please renew it before the expiry date to continue enjoying our services.";

//                        // Send email notification
//                        var emailSent = await SendEmailAsync(user.Email, "Payment Expiration Notice", message);

//                        if (emailSent)
//                        {
//                            // Store the notification in the database
//                            var notification = new PaymentNotification
//                            {
//                                Id = Guid.NewGuid(),
//                                UserId = user.Id,
//                                PaymentId = payment.Id,
//                                NotificationDate = DateTime.UtcNow,
//                                Message = message,
//                                IsEmailSent = true
//                            };

//                            _context.PaymentNotifications.Add(notification);
//                            await _context.SaveChangesAsync();

//                            _logger.LogInformation($"Sent expiration notice to {user.Email} for payment {payment.Id}.");
//                        }
//                        else
//                        {
//                            _logger.LogError($"Failed to send email to {user.Email} for payment {payment.Id}.");
//                        }
//                    }
//                }
//            }

//            // Helper method to send the email
//            public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
//            {
//                try
//                {
//                    var fromAddress = new MailAddress("mohanakahnan2k4@gmail.com", "UnicomFitness");
//                    var toAddress = new MailAddress(toEmail);
//                    const string fromPassword = "ngwd hbmj hzdv kzbs";  // Ideally, use secure storage for credentials

//                    var smtp = new SmtpClient
//                    {
//                        Host = "smtp.gmail.com",  // SMTP server for your email provider
//                        Port = 465,
//                        EnableSsl = true,
//                        Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
//                    };

//                    using (var message = new MailMessage(fromAddress, toAddress)
//                    {
//                        Subject = subject,
//                        Body = body
//                    })
//                    {
//                        await smtp.SendMailAsync(message);
//                        return true;
//                    }
//                }
//                catch (Exception ex)
//                {
//                    _logger.LogError($"Error sending email: {ex.Message}");
//                    return false;
//                }
//            }
//        }
//}
