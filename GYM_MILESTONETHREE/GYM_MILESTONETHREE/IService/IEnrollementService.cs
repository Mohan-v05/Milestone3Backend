using GYM_MILESTONETHREE.Models;
using GYM_MILESTONETHREE.RequestModels;
using GYM_MILESTONETHREE.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace GYM_MILESTONETHREE.IService
{
    public interface IEnrollementService
    {
        Task<EnrollementResponse> Addenrollments(NewEnrollementsReq enrollmentData);
        Task<List<GymPrograms>> GetProgramsForMemberAsync(int UserId);
    
        Task<Enrollments> deleteEnrollmentasync(Guid id); 
    }
}
