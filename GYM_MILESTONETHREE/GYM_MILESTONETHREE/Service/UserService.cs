using BCrypt.Net;
using GYM_MILESTONETHREE.DataBase;
using GYM_MILESTONETHREE.Enums;
using GYM_MILESTONETHREE.IRepository;
using GYM_MILESTONETHREE.IService;
using GYM_MILESTONETHREE.Models;
using GYM_MILESTONETHREE.RequestModels;
using GYM_MILESTONETHREE.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace GYM_MILESTONETHREE.Service
{
    public class UserService:IUserService
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
            var isValid = await _repository.Login(model);
            if (isValid == true) 
            {
                return createToken();
            }
            else
            {
                throw new Exception("check Your email and password");
            }
        }

        private TokenModel createToken()
        {
            var key = _config["Jwt:key"];
            var seckey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
            var credentials = new SigningCredentials(seckey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
               // claims: _config[],
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials
                );

            var res = new TokenModel();
            res.token = new JwtSecurityTokenHandler().WriteToken(token);
            return res;

        }

        public async Task<string>AddUser(AddUserReq req)
        {
            try
            {
                var newUser = new Users()
                {
                    Name = req.Name,
                    email = req.email,
                    Role = req.Role,
                    Nicnumber = req.Nicnumber,
                    PasswordHashed = BCrypt.Net.BCrypt.HashPassword(req.Password),
                    IsActivated = req.isActivated,
                };

                var data= await _repository.AddUser(newUser);
                return data;
            }
            catch(Exception ex) 
            {
              return ex.Message;
            }
        }
    }

}
