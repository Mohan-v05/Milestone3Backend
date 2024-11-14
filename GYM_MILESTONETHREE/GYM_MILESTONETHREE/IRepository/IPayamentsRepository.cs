using GYM_MILESTONETHREE.Controllers;
using GYM_MILESTONETHREE.Models;

namespace GYM_MILESTONETHREE.IRepository
{
    public interface IPayamentsRepository
    {
        Task<Payments> AddPayment(Payments payment);
    }
}
