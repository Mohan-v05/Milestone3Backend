using GYM_MILESTONETHREE.DataBase;
using GYM_MILESTONETHREE.IRepository;

namespace GYM_MILESTONETHREE.Repository
{
    public class EnrollmentRepository: IEnrollmentRepository
    {
        private readonly AppDb _context;

        public EnrollmentRepository(AppDb context)
        {
            _context = context;
        }
    }
}
