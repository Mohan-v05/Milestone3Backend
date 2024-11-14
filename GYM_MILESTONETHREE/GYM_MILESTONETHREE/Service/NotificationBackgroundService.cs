using static GYM_MILESTONETHREE.Service.PaymentService;

namespace GYM_MILESTONETHREE.Service
{
    public class NotificationBackgroundService : BackgroundService
    {
        private readonly PaymentNotificationService _paymentNotificationService;
        private readonly TimeSpan _runAt = TimeSpan.FromHours(8); // You can set the time you want this to run (e.g., 8 AM daily)

        public NotificationBackgroundService(PaymentNotificationService paymentNotificationService)
        {
            _paymentNotificationService = paymentNotificationService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var currentTime = DateTime.UtcNow;
                var nextRun = currentTime.Date.AddDays(1).Add(_runAt);

                // Wait until the scheduled time
                var delay = nextRun - currentTime;
                if (delay.TotalMilliseconds > 0)
                {
                    await Task.Delay(delay, stoppingToken);
                }

                // Send the notifications
                await _paymentNotificationService.SendExpirationNotificationsAsync();
            }
        }
    }
}
