using GYM_MILESTONETHREE.DataBase;
using GYM_MILESTONETHREE.IRepository;

namespace GYM_MILESTONETHREE.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly AppDb _context;

        public UserRepository(AppDb context)
        {
            _context = context;
        }
    }
}
