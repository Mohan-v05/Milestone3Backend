using GYM_MILESTONETHREE.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GYM_MILESTONETHREE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentNotificationController : ControllerBase
    {

        private readonly PaymentNotificationService _paymentNotificationService;
        private readonly ILogger<PaymentNotificationController> _logger;

        public PaymentNotificationController(
            PaymentNotificationService paymentNotificationService,
            ILogger<PaymentNotificationController> logger)
        {
            _paymentNotificationService = paymentNotificationService;
            _logger = logger;
        }

        // Endpoint to trigger payment expiration notifications
        [HttpGet("send-expiration-notifications")]
        public async Task<IActionResult> SendExpirationNotifications()
        {
            try
            {
                // Call the service to send expiration notifications
                await _paymentNotificationService.SendExpirationNotificationsAsync();
                return Ok("Expiration notifications sent successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in sending expiration notifications: {ex.Message}");
                return StatusCode(500, "An error occurred while sending notifications.");
            }
        }
        [HttpGet("custom mail")]
        public async Task<IActionResult> SendCustomeMail()
        {
            await _paymentNotificationService.SendEmailAsync("ut01146tic@gmail.com", "abcd", "efg");
            return Ok();
        }
    }
}
