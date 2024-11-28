using GYM_MILESTONETHREE.Models;

namespace GYM_MILESTONETHREE.IRepository
{
    public interface IEnrollmentRepository
    {
        Task<List<Enrollments>> AddEnrollmentsAsync(List<Enrollments> enrollments);

        Task<List<GymPrograms>> GetProgramsByUserIDAsync(int userID);

        Task<List<Users>> GetUsersByProgramIDAsync(int programId);

        Task<Enrollments> deleteEnrollmentasync(Enrollments enrollment);

        Task<Enrollments> getEnrollmentsById(Guid id);
    }
}
