using GYM_MILESTONETHREE.Enums;
using GYM_MILESTONETHREE.IRepository;
using GYM_MILESTONETHREE.IService;
using GYM_MILESTONETHREE.Models;
using GYM_MILESTONETHREE.Repository;
using GYM_MILESTONETHREE.RequestModels;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<string> AddPayment(PaymentsReq req)
        {
            try
            {
                var user = await _userrepository.GetUserByIdAsync(req.memberid);
                var admin = await _userrepository.GetUserByIdAsync(req.recievedBy);
                Decimal Balance = 0;
                Notification notification = new Notification();
                notification.Title = "Payment received";
                notification.status=true;
                notification.isRead=false;

               
                if (user != null && admin!=null)
                {
                    Payments payment = new Payments();
                    {
                        payment.UserId = req.memberid;
                        payment.dateTime= DateTime.Now;
                        payment.receiverId = req.recievedBy;

                        notification.UserId = req.memberid;
                        if (req.PaymentType == PaymentType.MonthlySubscribtionrenewal)
                        {   
                            
                            user.ExpiryDate = DateTime.Now.AddMonths(req.quantity);
                            payment.Description = user.Name+ " Monthly payment received By" + admin.Name;
                          
                            payment.Amount = (Decimal)user.Fees * req.quantity;

                            Balance = (decimal)(req.Amount - payment.Amount);
                            //Generate notification
                            notification.Message = $"Dear {user.Name} We have received your amount of {payment.Amount} on {payment.dateTime} your monthly membership is valid till {user.ExpiryDate} ";

                        }
                        else
                        if (req.PaymentType == PaymentType.AnnualSubcribtionrenewal)
                        {
                            decimal SubscribtionOffer = req.quantity*(Decimal)user.Fees * 12 * 20/100;
                           
                            user.ExpiryDate = DateTime.Now.AddYears(req.quantity);
                            payment.Amount = (Decimal)user.Fees * req.quantity*12;
                            Balance = (decimal)(req.Amount - payment.Amount - SubscribtionOffer);
                            payment.Description = req.memberid + "Annual payment received By" + req.recievedBy;

                            //Generate notification
                            notification.Message = $"Dear {user.Name} We have received your amount of {payment.Amount} on {payment.dateTime} your Anual membership is valid till {user.ExpiryDate} ";
                        }
                        else
                        if (req.PaymentType == PaymentType.Initialpayment)
                        {
                            payment.Amount = 1000;
                            Balance= req.Amount-1000;
                            payment.Description = $"Initial fee paid \n Payee:{user.Name} \n receiver:{admin.Name}";
                            user.IsActivated = true;

                            notification.Message = $"Dear {user.Name} We have received your amount of {payment.Amount} on {payment.dateTime} your Now an member of Unicom Fitness\n Thanks For deal with us";
                        
                        }
                        var notificationdata = await _notificationService.AddNotificationAsync(notification);
                        var data = await _paymentsRepository.AddPayment(payment);
                        var userdata = await _userrepository.updateUser(user);

                    }

                    return $"Payment succesful  \n Amount paid:{req.Amount}\n Balance:  {Balance} {notification.Message}";
                }
                else
                {
                    return "user notFound";
                }


            }
            catch (Exception ex)
            {
                return ex.Message;
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


