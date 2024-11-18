using GYM_MILESTONETHREE.IRepository;
using GYM_MILESTONETHREE.IService;
using GYM_MILESTONETHREE.Models;
using GYM_MILESTONETHREE.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GYM_MILESTONETHREE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollementController : ControllerBase
    {
        private readonly IEnrollementService _enrollementService;

        public EnrollementController(IEnrollementService enrollementService)
        {
            _enrollementService = enrollementService;
        }

        [HttpPost]
        public async Task<IActionResult> AddEnrollement(NewEnrollementsReq newEnrollementsReq)
        {
            try
            {
               var data= await _enrollementService.Addenrollments(newEnrollementsReq);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetProgramsForMemberAsync(int memberId)
        {
            try
            {
                var data = await _enrollementService.GetProgramsForMemberAsync(memberId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }
}
