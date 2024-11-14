using GYM_MILESTONETHREE.Models;

namespace GYM_MILESTONETHREE.IRepository
{
    public interface IEnrollmentRepository
    {
        Task<List<Enrollments>> AddEnrollmentsAsync(List<Enrollments> enrollments);
       Task<List<GymPrograms>> GetProgramsByUserIDAsync(int userID);
    }
}
