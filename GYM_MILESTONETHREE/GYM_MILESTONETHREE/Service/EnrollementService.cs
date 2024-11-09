using GYM_MILESTONETHREE.DataBase;
using GYM_MILESTONETHREE.IRepository;

namespace GYM_MILESTONETHREE.Service
{
    public class EnrollementService
    {
       private readonly IEnrollmentRepository _enrollmentRepository;

        public EnrollementService(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }
    }
}
