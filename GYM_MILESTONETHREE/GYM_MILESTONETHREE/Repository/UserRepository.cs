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



        public async Task<Users> AddUser(Users user) 
        {
            if (user == null)
            {
                throw  new Exception( "user cant be null");
            }
            else
            {
              var data= await _context.AddAsync(user);
              await _context.SaveChangesAsync();
                return data.Entity;
            }
        }

    
        public async Task<Users> GetUserByIdAsync(int userId)
        {
            
            var user = await _context.users.Include(u => u.Address).Include(u=>u.Payments).Include(u=>u.Enrollment).ThenInclude(e=>e.GymProgram).Include(u=>u.Notification).FirstOrDefaultAsync(u=>u.Id==userId);

            
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }

            return user;
        }
        public async Task<Users> updateUser(Users user)
        {    
            if (user != null)
            {
                var data = _context.users.Update(user);
                await _context.SaveChangesAsync();
                return data.Entity;
            }
            else
            {
                throw new Exception("User is null");
            }
        }
        public async Task<List<Users>> GetAllUsersAsync()
        {
            return await _context.users.Include(u=>u.Address).Include(u=>u.Enrollment).ToListAsync();
        }

        public async Task<List<Users>>GetActiveUsersAsync()
        {
           var Activeusers= await _context.users.Where(u => u.IsActivated==true).ToListAsync();
            return Activeusers;
        }

        
        public async Task<List<Users>> SoftDeleteExpiredUsersAsync()
        {
            

                var dateThreshold = DateTime.UtcNow.AddDays(-5);


                var usersToDeactivate = await _context.users
                    .Where(u => u.ExpiryDate != null && u.ExpiryDate.Value <= dateThreshold && u.IsActivated)
                    .ToListAsync();

                foreach (var user in usersToDeactivate)
                {
                    user.IsActivated = false;
                }
                if (usersToDeactivate.Count > 0)
            {
                _context.UpdateRange(usersToDeactivate);
                await _context.SaveChangesAsync();
                return usersToDeactivate;
            }
            else
            {
                throw new Exception("No one to deactivate");
            }
                       
        }
        public async Task<Users> DeleteUserByIdAsync(Users user)
        {
             _context.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }


    }

}

