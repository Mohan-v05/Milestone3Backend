using GYM_MILESTONETHREE.DataBase;
using GYM_MILESTONETHREE.IRepository;
using GYM_MILESTONETHREE.Models;
using GYM_MILESTONETHREE.RequestModels;
using Microsoft.EntityFrameworkCore;

namespace GYM_MILESTONETHREE.Repository
{
    public class UserRepository: IUserRepository
    {
        private readonly AppDb _context;

        public UserRepository(AppDb context)
        {
            _context = context;
        }
        public async Task<Boolean> Login(loginModel loginModel)
        {
            var user = await _context.users.SingleOrDefaultAsync(r => r.email == loginModel.email);
            if (user != null)
            {

                var IsValid = BCrypt.Net.BCrypt.Verify(loginModel.password, user.PasswordHashed);
                if (IsValid)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }



        public async Task<string> AddUser(Users user) 
        {
            if (user == null)
            {
                return "user cant be null";
            }
            else
            {
              var data= await _context.AddAsync(user);
              await _context.SaveChangesAsync();
                return "User Added Succesfull";
            }
        }
       
    }
}
