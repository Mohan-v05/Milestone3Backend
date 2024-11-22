using GYM_MILESTONETHREE.Models;
using GYM_MILESTONETHREE.RequestModels;

namespace GYM_MILESTONETHREE.IRepository
{
    public interface IUserRepository
    {
        Task<Users> AddUser(Users user);
  
        Task<Users> GetUserByIdAsync(int userId);
      
        Task<Users> GetUserByEmail(string email);

        Task<Users> updateUser(Users user);

        Task<List<Users>> GetAllUsersAsync();

        Task<Users> DeleteUserByIdAsync(Users user);

        Task<List<Users>> GetActiveUsersAsync();

        Task<List<Users>> SoftDeleteExpiredUsersAsync();

    }
}
