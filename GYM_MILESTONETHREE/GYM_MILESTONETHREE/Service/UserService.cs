using GYM_MILESTONETHREE.DataBase;

namespace GYM_MILESTONETHREE.Service
{
    public class UserService
    {
        private readonly AppDb _context;

        public UserService(AppDb context)
        {
            _context = context;
        }
    }
}
