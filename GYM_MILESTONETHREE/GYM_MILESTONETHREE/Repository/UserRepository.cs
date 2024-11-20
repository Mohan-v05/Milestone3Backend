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
        public async Task<Users> GetUserByEmail(string email)
        {
            var user = await _context.users.SingleOrDefaultAsync(r => r.Email == email);
            return user;
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

    
        public async Task<Users> GetUserByIdAsync(int userId)
        {
            
            var user = await _context.users.FindAsync(userId);

            
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

            return user;
        }
        public async Task<string> updateUser(Users user)
        {
            try
            {
                _context.users.Update(user);
                await _context.SaveChangesAsync();
                return "user updated succesful";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<List<Users>> GetAllUsersAsync()
        {
            return await _context.users.Include(u=>u.Address).ToListAsync();
        }
        public async Task<List<Users>>GetActiveUsersAsync()
        {
           var Activeusers= await _context.users.Where(u => u.IsActivated==true).ToListAsync();
            return Activeusers;
        }

        
        public async Task<bool> SoftDeleteExpiredUsersAsync()
        {
            try {
                var dateThreshold = DateTime.UtcNow.AddDays(-5);


                var usersToDeactivate = await _context.users
                    .Where(u => u.ExpiryDate != null && u.ExpiryDate.Value <= dateThreshold && u.IsActivated)
                    .ToListAsync();

                foreach (var user in usersToDeactivate)
                {
                    user.IsActivated = false;
                }

                _context.UpdateRange(usersToDeactivate);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }
           
        }

    }

}

