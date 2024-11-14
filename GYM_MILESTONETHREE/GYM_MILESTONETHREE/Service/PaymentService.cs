using GYM_MILESTONETHREE.Enums;
using GYM_MILESTONETHREE.IRepository;
using GYM_MILESTONETHREE.IService;
using GYM_MILESTONETHREE.Models;
using GYM_MILESTONETHREE.Repository;
using GYM_MILESTONETHREE.RequestModels;
using System.Net.Mail;
using System.Net;

namespace GYM_MILESTONETHREE.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IPayamentsRepository _paymentsRepository;
        private readonly IUserRepository _userrepository;

        public PaymentService(IPayamentsRepository paymentsRepository, IUserRepository userrepository)
        {
            _paymentsRepository = paymentsRepository;
            _userrepository = userrepository;
        }

        public async Task<string> AddPayment(PaymentsReq req)
        {
            try
            {
                var user = await _userrepository.GetUserByIdAsync(req.memberid);

                var admin = await _userrepository.GetUserByIdAsync(req.recievedBy);
                if (user != null)
                {
                    Payments payment = new Payments();
                    {
                        payment.UserId = req.memberid;
                        payment.Amount = req.Amount;
                        payment.receiverId = req.recievedBy;
                        if (req.PaymentType == PaymentType.MonthlySubscribtionrenewal)
                        {
                            payment.ExpiryDate = DateTime.Now.AddMonths(req.quantity);
                            payment.Description = req.memberid + " Monthly payment received By" + req.recievedBy;
                        }
                        else
                        if (req.PaymentType == PaymentType.AnnualSubcribtionrenewal)
                        {
                            payment.ExpiryDate = DateTime.Now.AddYears(req.quantity);
                            payment.Description = req.memberid + "Annual payment received By" + req.recievedBy;
                        }
                        else
                        if (req.PaymentType == PaymentType.Initialpayment)
                        {
                            payment.Description = $"Initial fee paid \n Payee:{user.Name} \n receiver:{admin.Name}";
                            user.IsActivated = true;
                        }

                        var data = await _paymentsRepository.AddPayment(payment);

                    }
                    return "Payment succesful";
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


