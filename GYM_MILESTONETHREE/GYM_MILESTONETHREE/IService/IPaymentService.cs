using GYM_MILESTONETHREE.RequestModels;

namespace GYM_MILESTONETHREE.IService
{
    public interface IPaymentService
    {
        Task<string> AddPayment(PaymentsReq req);
    }
}
