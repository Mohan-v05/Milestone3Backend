using GYM_MILESTONETHREE.Models;
using GYM_MILESTONETHREE.ResponseModels;

namespace GYM_MILESTONETHREE.IService
{
    public interface IGymProgramService
    {
        Task<GymPrograms> GetGymProgramsbyIdAsync(int id);
        Task<List<Gymprogramsresponse>> GetAllGymProgramsAsync();
    }
}
