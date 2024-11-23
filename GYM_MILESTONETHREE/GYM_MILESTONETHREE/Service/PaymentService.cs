using GYM_MILESTONETHREE.Enums;
using GYM_MILESTONETHREE.IRepository;
using GYM_MILESTONETHREE.IService;
using GYM_MILESTONETHREE.Models;
using GYM_MILESTONETHREE.Repository;
using GYM_MILESTONETHREE.RequestModels;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using GYM_MILESTONETHREE.ResponseModels;

namespace GYM_MILESTONETHREE.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IPayamentsRepository _paymentsRepository;
        private readonly IUserRepository _userrepository;
        private readonly INotificationRepository _notificationService;
        public PaymentService(IPayamentsRepository paymentsRepository, IUserRepository userrepository, INotificationRepository notificationService)
        {
            _paymentsRepository = paymentsRepository;
            _userrepository = userrepository;
            _notificationService = notificationService;
        }
        public async Task<PaymentResponse> AddPayment(PaymentsReq req)
        {
            var user = await _userrepository.GetUserByIdAsync(req.memberid);
            var admin = await _userrepository.GetUserByIdAsync(req.recievedBy);

            if (user == null || admin == null)
            {
                throw new Exception("User or Admin not Found");
            }

            if (user.Fees == null)
            {
                throw new Exception("Enroll First");
            }

            Notification notification = new Notification
            {
                Title = "Payment received",
                status=true,
                isRead=false,
                UserId = req.memberid
            };

            Payments payment = new Payments
            {
                PayerId = req.memberid,
                dateTime = DateTime.Now,
                PayeeId = req.recievedBy,
                PaymentType = req.PaymentType
            };

            // Handle payment based on payment type
            if (req.PaymentType == PaymentType.MonthlySubscribtionrenewal)
            {
                await ProcessMonthlySubscription(req, user, admin, payment, notification);
            }
            else if (req.PaymentType == PaymentType.AnnualSubcribtionrenewal)
            {
                await ProcessAnnualSubscription(req, user, admin, payment, notification);
            }
            else if (req.PaymentType == PaymentType.Initialpayment)
            {
                await ProcessInitialPayment(req, user, admin, payment, notification);
            }

            // Save payment and notification
            await _notificationService.AddNotificationAsync(notification);
            await _paymentsRepository.AddPayment(payment);
            await _userrepository.updateUser(user);

            return new PaymentResponse
            {
                Id = payment.Id,
                PayerName = user.Name,
                PayeeName = admin.Name,
                PaymentDate = payment.dateTime,
                Quantity = req.quantity,
                Description = payment.Description
            };
        }
        private async Task ProcessMonthlySubscription(PaymentsReq req, Users user, Users admin, Payments payment, Notification notification)
        {
            // Update the user's expiry date
            if (user.ExpiryDate == null)
            {
                user.ExpiryDate = DateTime.Now.AddMonths(req.quantity);
            }
            else
            {
                user.ExpiryDate = user.ExpiryDate.Value.AddMonths(req.quantity);
            }

            decimal Total = (Decimal)user.Fees * req.quantity;

            // Calculate payment amount based on discount
            payment.Amount= Total - req.AnyDiscount;

            // Construct payment description
            payment.Description = $"{user.Name} Purchaches Monthly subscription , unit price: {user.Fees} Quantity: {req.quantity} Total: {Total} - Discount: {req.AnyDiscount} Amount paid: {req.Amount} Balance: {req.Amount - payment.Amount} Month payment. Total received By {admin.Name} Valid till {user.ExpiryDate}";

            // Generate notification message
            notification.Message = $"Dear {user.Name}, We have received your payment of {payment.Amount} on {payment.dateTime}. Your monthly membership is valid till {user.ExpiryDate}.";
        }

        private async Task ProcessAnnualSubscription(PaymentsReq req, Users user, Users admin, Payments payment, Notification notification)
        {
            // Update the user's expiry date
            user.ExpiryDate = user.ExpiryDate == null ? DateTime.Now.AddYears(req.quantity) : user.ExpiryDate.Value.AddYears(req.quantity);

            //calculate total
            decimal Total = (Decimal)user.Fees * req.quantity * 12;

            // Calculate payment amount based on discount
            payment.Amount = Total - req.AnyDiscount;
              

            // Construct payment description
            payment.Description = $"{user.Name} purchased Annual subscription, paid unit price: {Total} Quantity: {req.quantity} Total: {user.Fees * req.quantity * 12} - Discount: {req.AnyDiscount} Amount paid: {req.Amount} Balance: {req.Amount - payment.Amount} received By {admin.Name}Valid till {user.ExpiryDate}";

            // Generate notification message
            notification.Message = $"Dear {user.Name}, We have received your payment of {payment.Amount} on {payment.dateTime}. Your annual membership is valid till {user.ExpiryDate}.";
        }
        private async Task ProcessInitialPayment(PaymentsReq req, Users user, Users admin, Payments payment, Notification notification)
        {
            // Set initial payment amount
            decimal initalPaymentAmount = 1000;
            
            payment.Amount = initalPaymentAmount - req.AnyDiscount;
            if (req.Amount - payment.Amount >= 0)
            {
                // Construct payment description
                payment.Description = $"Initial fee paid by Payer: {user.Name} Payee: {admin.Name} Initial fee amount: {initalPaymentAmount} - Discount: {req.AnyDiscount} Balance: {req.Amount - payment.Amount}";

                // Activate the user
                user.IsActivated = true;

                // Generate notification message
                notification.Message = $"Dear {user.Name}, We have received your payment of {payment.Amount} on {payment.dateTime}. You are now a member of Unicom Fitness. Thanks for doing business with us.";

            }
            else
            {
                throw new Exception(" Insufficient amount ");
            }
             
        }


        public async Task<IEnumerable<Payments>> GetAllPaymentsAsync()
        {
            return await _paymentsRepository.GetAllPaymentsAsync();
        }

        public async Task<Payments> GetPaymentByIdAsync(Guid paymentId)
        {
            return await _paymentsRepository.GetPaymentByIdAsync(paymentId);
        }

    }
}


