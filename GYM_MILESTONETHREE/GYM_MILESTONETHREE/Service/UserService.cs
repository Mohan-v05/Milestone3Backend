using BCrypt.Net;
using GYM_MILESTONETHREE.DataBase;
using GYM_MILESTONETHREE.Enums;
using GYM_MILESTONETHREE.IRepository;
using GYM_MILESTONETHREE.IService;
using GYM_MILESTONETHREE.Models;
using GYM_MILESTONETHREE.Repository;
using GYM_MILESTONETHREE.RequestModels;
using GYM_MILESTONETHREE.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Generators;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GYM_MILESTONETHREE.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _config;

        public UserService(IUserRepository repository, IConfiguration config)
        {
            _repository = repository;
            _config = config;
        }

        public async Task<TokenModel> login(loginModel model)
        {
            var User = await _repository.GetUserByEmail(model.email);
            if (User != null)
            {
                var IsValid = BCrypt.Net.BCrypt.Verify(model.password, User.PasswordHashed);
                if (IsValid)
                {
                    return createToken(User);
                }
                else
                {
                    throw new Exception("check Your password");
                }
            }
            else
            {
                throw new Exception("User Not Found");
            }
        }

        private TokenModel createToken(Users user)
        {
           
            var key = _config["Jwt:key"];
            var seckey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
            var credentials = new SigningCredentials(seckey, SecurityAlgorithms.HmacSha256);

            var claimsList = new List<Claim>();
            claimsList.Add(new Claim("Id", user.Id.ToString()));
            claimsList.Add(new Claim("Name", user.Name));
            claimsList.Add(new Claim("Email", user.Email));
            claimsList.Add(new Claim("Role", user.Role.ToString()));
            claimsList.Add(new Claim(ClaimTypes.Role, user.Role.ToString()));

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claimsList,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials
                );

            var res = new TokenModel();
            res.token = new JwtSecurityTokenHandler().WriteToken(token);
            return res;

        }

        public async Task<Users> AddUser(AddUserReq req)
        {
            try
            {
                var newAddress = new Address();
                if (req.Address!=null)
                {
                    {
                        newAddress.firstLine = req.Address.firstLine;
                        newAddress.secondLine = req.Address.secondLine;
                        newAddress.city = req.Address.city;
                    };

                }
                
                var newUser = new Users()
                { 
                    Name = req.Name,
                    Email = req.email,
                    Role = req.Role,
                    Nicnumber = req.Nicnumber,
                    Gender = req.gender,
                    Address = newAddress,
                    PasswordHashed = BCrypt.Net.BCrypt.HashPassword(req.Password),
                    IsActivated = req.isActivated,
                };

                var data = await _repository.AddUser(newUser);
                return newUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Users> GetUserByIdAsync(int id)
        {
            try
            {
                var data = await _repository.GetUserByIdAsync(id);
                if (data != null)
                {
                    return data;
                }
                else
                {
                    throw new Exception("User not found");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while fetching the user.", ex);
            }
        }
        public async Task<List<Users>> GetAllUsersAsync()
        {
            return await _repository.GetAllUsersAsync();
        }

        public async Task<List<Users>> SoftDeleteExpiredUsersAsync()
        {
           var data= await _repository.SoftDeleteExpiredUsersAsync();
            return data;
        }

        public async Task<List<Users>> GetActiveUsersAsync()
        {
            return await _repository.GetActiveUsersAsync();
        }

        public async Task<Users> DeleteUserByIdAsync(int UserId , bool permanent)
        {
            var data = await _repository.GetUserByIdAsync(UserId);
           
            if (data != null)
                if (permanent)
                {
                    return await _repository.DeleteUserByIdAsync(data);
                }
            else
                {
                data.IsActivated = false;
               return await _repository.updateUser(data);
                 }
            else
            {
                throw new Exception($"Unable to Find User with Id: {UserId}");

            }
        }
        public async Task<Users> updateUserAsync(int userId, UpdateUser updateUserdata)
        {
            var oldUserInfo = await _repository.GetUserByIdAsync(userId);

            if (oldUserInfo == null)
            {
                throw new Exception("User not found");
            }

            oldUserInfo.Name = updateUserdata.Name;
            oldUserInfo.Email = updateUserdata.Email;
            oldUserInfo.Role = updateUserdata.Role ?? oldUserInfo.Role;
            oldUserInfo.Nicnumber = updateUserdata.Nicnumber;


            if (updateUserdata.Address != null)
            {
                oldUserInfo.Address.firstLine = updateUserdata.Address.firstLine ;
                oldUserInfo.Address.secondLine = updateUserdata.Address.secondLine;
                oldUserInfo.Address.city = updateUserdata.Address.city ;
            }

            oldUserInfo.Gender = updateUserdata.Gender ?? oldUserInfo.Gender;

            var updatedUser = await _repository.updateUser(oldUserInfo);
            return updatedUser;
        }

        public async  Task<Users> updatePassword(UpdatePasswordReq updateUser)
        {
            var oldUserInfo = await _repository.GetUserByIdAsync(updateUser.id);
            if (oldUserInfo != null)
            {
                if (BCrypt.Net.BCrypt.Verify(updateUser.oldPassword,oldUserInfo.PasswordHashed) && updateUser.nic==oldUserInfo.Nicnumber)
                {
                    oldUserInfo.PasswordHashed = BCrypt.Net.BCrypt.HashPassword(updateUser.newPassword);
                    var updatedUserinfo= await _repository.updateUser(oldUserInfo);
                    return updatedUserinfo;
                }
                throw new Exception("old password or Nic not match");
                
            }
            else
            {
                throw new Exception("User not found");
            }
              
            
           

        }

    }
}
