namespace GYM_MILESTONETHREE.Models
{
    public class PaymentNotification
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }  // Foreign Key to Users
        public Users User { get; set; }  // Navigation property
        public Guid PaymentId { get; set; }  // Foreign Key to Payment
        public Payments Payment { get; set; }  // Navigation property to Payment
        public DateTime NotificationDate { get; set; }  // Date when the notification was sent
        public string Message { get; set; }  // The message sent to the user
        public bool IsEmailSent { get; set; }  // Whether the email was successfully sent
    }
}
