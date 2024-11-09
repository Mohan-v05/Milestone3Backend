using GYM_MILESTONETHREE.DataBase;
using GYM_MILESTONETHREE.IRepository;

namespace GYM_MILESTONETHREE.Repository
{
    public class GymProgramsRepository: IGymProgramRepository
    {
        private readonly AppDb _context;

        public GymProgramsRepository(AppDb context)
        {
            _context = context;
        }
    }
}
