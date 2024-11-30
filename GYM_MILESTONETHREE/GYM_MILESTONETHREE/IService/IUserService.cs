using GYM_MILESTONETHREE.Models;
using GYM_MILESTONETHREE.RequestModels;
using GYM_MILESTONETHREE.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GYM_MILESTONETHREE.IService
{
    public interface IUserService
    {
        Task<Users> AddUser(AddUserReq req);

        Task<TokenModel> login(loginModel model);

        Task<Users> GetUserByIdAsync(int id);

        Task<List<Users>> GetAllUsersAsync();

        Task<List<Users>> GetActiveUsersAsync();

        Task<Users> updateUserAsync(int UserId,UpdateUser updateUser);

        Task<List<Users>> SoftDeleteExpiredUsersAsync();

        Task<Users> DeleteUserByIdAsync(int userId,bool permanent);

        Task<Users> updatePassword(UpdatePasswordReq updateUser);
    }
}
