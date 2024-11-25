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
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Ocsp;
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
                var Address = new Address()
                {
                    firstLine = req.Address.firstLine,
                    secondLine = req.Address.secondLine,
                    city = req.Address.city,
                };
                var newUser = new Users()
                { 
                    Name = req.Name,
                    Email = req.email,
                    Role = req.Role,
                    Nicnumber = req.Nicnumber,
                    Gender = req.gender,
                    Address = Address,
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

        public async Task<Users> DeleteUserByIdAsync(int UserId)
        {
            var data = await _repository.GetUserByIdAsync(UserId);
            data.IsActivated = false;
            if (data != null)
            {
                return await _repository.DeleteUserByIdAsync(data);
            }
            else
            {
                throw new Exception($"Unable to Find User with Id: {UserId}");

            }
        }
        public async Task<Users> ChangePasswordAsync(ChangePasswordRequest request)
        {
            var data= await _repository.GetUserByIdAsync(request.Id);
            if(data != null)
            {
                if (data.Nicnumber == request.Nic)
                {
                    var IsValid = BCrypt.Net.BCrypt.Verify(request.OldPassword, data.PasswordHashed);

                    if (IsValid)
                    {
                        data.PasswordHashed = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
                        var response = await _repository.updateUser(data);
                        return response;
                    }
                    else
                    {
                        throw (new Exception("incorrectpassword"));
                    }
                }
                else
                {
                    throw new Exception("invalid Nic");
                }


            }
            else
            {
                throw new Exception("User not founf");
            }

        }

    }
}
