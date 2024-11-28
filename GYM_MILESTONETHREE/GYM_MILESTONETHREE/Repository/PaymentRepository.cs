using GYM_MILESTONETHREE.Controllers;
using GYM_MILESTONETHREE.DataBase;
using GYM_MILESTONETHREE.IRepository;
using GYM_MILESTONETHREE.Models;
using Microsoft.EntityFrameworkCore;

namespace GYM_MILESTONETHREE.Repository
{
    public class PaymentRepository : IPayamentsRepository
    {
        private readonly AppDb _context;

        public PaymentRepository(AppDb context)
        {
            _context = context;
        }

        public async Task<Payments> AddPayment(Payments payment)
        {
            var data = await _context.payments.AddAsync(payment);
            await _context.SaveChangesAsync();
            return data.Entity;
        }

        public async Task<IEnumerable<Payments>> GetAllPaymentsAsync()
        {
            return await _context.payments.Include(p=>p.Payer).Include(p => p.Payee).ToListAsync();

        }

        public async Task<Payments> GetPaymentByIdAsync(Guid paymentId)
        {
            var data= await _context.payments.Include(p => p.Payer).FirstOrDefaultAsync(p => p.Id == paymentId);
            return data;
        }

    }
}
