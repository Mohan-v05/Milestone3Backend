using GYM_MILESTONETHREE.IService;
using GYM_MILESTONETHREE.Models;
using GYM_MILESTONETHREE.Repository;
using GYM_MILESTONETHREE.RequestModels;
using GYM_MILESTONETHREE.ResponseModels;
using GYM_MILESTONETHREE.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;

namespace GYM_MILESTONETHREE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController: ControllerBase
    {

        private readonly SendmailService _sendmailService;

        private readonly IUserService _userService;

        public UserController(IUserService userService, SendmailService sendmailService)
        {
            _userService = userService;
            _sendmailService = sendmailService;
        }

        [HttpPost]
        public async Task<IActionResult> Sendmail(SendmailRequest sendMailRequest)
        {
            try
            {
              
                await _sendmailService.Sendmail(sendMailRequest).ConfigureAwait(false);

               
                return Ok("Email was sent successfully");
            }
            catch (SocketException ex)
            {
                
                Console.WriteLine($"SocketException: {ex.Message}");

                
                return StatusCode(500, "Network issue while sending the email. Please try again later.");
            }
            catch (Exception ex)
            {
              
                Console.WriteLine($"Exception: {ex.Message}");

                
                return StatusCode(500, "An error occurred while processing your request.");
            }
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
                var errormsg = new Error()
                {
                    message = ex.Message
                };
                return BadRequest(errormsg);
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

        [HttpPut("{userId}")]
        public async Task<IActionResult> updateUserAsync(int userId,UpdateUser updateDetails)
        {
            try
            {
                var updatedUser = await _userService.updateUserAsync(userId,updateDetails);
                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException);
            }

        }

        [HttpPatch]
        public async Task<IActionResult>updatePassword(UpdatePasswordReq updateDetails)
        {
            try
            {
                var Updatedstatus =await _userService.updatePassword(updateDetails);
                return Ok(Updatedstatus);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
