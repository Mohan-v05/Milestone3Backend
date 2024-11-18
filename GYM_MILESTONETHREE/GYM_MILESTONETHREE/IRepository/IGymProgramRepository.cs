using GYM_MILESTONETHREE.DataBase;
using GYM_MILESTONETHREE.Models;

namespace GYM_MILESTONETHREE.IRepository
{
    public interface IGymProgramRepository
    {
        Task <GymPrograms> GetGymProgramsbyIdAsync(int id);
        Task<List<GymPrograms>> GetAllGymProgramsAsync();
    }
}
