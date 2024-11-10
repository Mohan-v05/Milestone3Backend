using GYM_MILESTONETHREE.Models;
using GYM_MILESTONETHREE.RequestModels;

namespace GYM_MILESTONETHREE.IRepository
{
    public interface IUserRepository
    {
        Task<string> AddUser(Users user);
        Task<Boolean> Login(loginModel loginModel);
    }
}
