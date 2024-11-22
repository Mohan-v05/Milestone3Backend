using GYM_MILESTONETHREE.Models;
using GYM_MILESTONETHREE.RequestModels;
using GYM_MILESTONETHREE.ResponseModels;

namespace GYM_MILESTONETHREE.IService
{
    public interface IPaymentService
    {
        Task<PaymentResponse> AddPayment(PaymentsReq req);
        Task<IEnumerable<Payments>> GetAllPaymentsAsync();
        Task<Payments> GetPaymentByIdAsync(Guid paymentId);
       
    }
}
