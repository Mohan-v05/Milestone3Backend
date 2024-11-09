using GYM_MILESTONETHREE.DataBase;

namespace GYM_MILESTONETHREE.Service
{
    public class EnrollementService
    {
        private readonly AppDb _context;

        public EnrollementService(AppDb context)
        {
            _context = context;
        }

    }
}
