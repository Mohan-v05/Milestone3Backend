using GYM_MILESTONETHREE.Models;
using GYM_MILESTONETHREE.RequestModels;
using GYM_MILESTONETHREE.ResponseModels;

namespace GYM_MILESTONETHREE.IService
{
    public interface IEnrollementService
    {
        Task<EnrollementResponse> Addenrollments(NewEnrollementsReq enrollmentData);
        Task<List<GymPrograms>> GetProgramsForMemberAsync(int UserId);
    }
}
