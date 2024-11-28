using GYM_MILESTONETHREE.IService;
using GYM_MILESTONETHREE.Models;
using GYM_MILESTONETHREE.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GYM_MILESTONETHREE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> login(loginModel Logincredential)
        {
            try
            {
                var data = await _userService.login(Logincredential);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost("addNewUser")]
        public async Task<IActionResult> AddUser(AddUserReq req)
        {
            try
            {
                var data = await _userService.AddUser(req);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetUserbyId/{id}")]
        public async Task<IActionResult> GetUserByIdAsync(int id)
        {
            try
            {
                var data = await _userService.GetUserByIdAsync(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("Getall")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            try
            {
                var data = await _userService.GetAllUsersAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetAllActive")]
        public async Task<IActionResult> GetActiveUsersAsync()
        {
            try
            {
                var data = await _userService.GetActiveUsersAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> SoftDeleteExpiredUsersAsync()
        {
            var data = await _userService.SoftDeleteExpiredUsersAsync();
            return Ok(data);
        }


        [HttpDelete("{userId}/{permanent}")]
        public async Task<IActionResult> SoftDelete(int userId, bool permanent)
        {
            try
            {
                var data = await _userService.DeleteUserByIdAsync(userId, permanent);
                return Ok(data);
            }
            catch (Exception ex)
            {
                {
                    return BadRequest(ex.Message);
                }
            }
        }





    }
}
