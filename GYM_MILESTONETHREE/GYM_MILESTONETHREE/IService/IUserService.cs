using GYM_MILESTONETHREE.RequestModels;
using GYM_MILESTONETHREE.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace GYM_MILESTONETHREE.IService
{
    public interface IUserService
    {
       Task<string> AddUser(AddUserReq req);
       Task<TokenModel> login(loginModel model);
    }
}
