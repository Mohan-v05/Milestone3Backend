using GYM_MILESTONETHREE.Models;
using GYM_MILESTONETHREE.RequestModels;

namespace GYM_MILESTONETHREE.IService
{
    public interface IPaymentService
    {
        Task<string> AddPayment(PaymentsReq req);
        Task<IEnumerable<Payments>> GetAllPaymentsAsync();
        Task<Payments> GetPaymentByIdAsync(Guid paymentId);
       
    }
}
