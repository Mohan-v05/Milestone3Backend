using GYM_MILESTONETHREE.DataBase;
using GYM_MILESTONETHREE.IService;

namespace GYM_MILESTONETHREE.Service
{
    public class ProgramService : IProgramService
    {
        private readonly AppDb _context;

        public ProgramService(AppDb context)
        {
            _context = context;
        }


    }
}
