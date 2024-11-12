using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GYM_MILESTONETHREE.DataBase;
using GYM_MILESTONETHREE.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using GYM_MILESTONETHREE.RequestModels;

namespace GYM_MILESTONETHREE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GymProgramsController : ControllerBase
    {
        private readonly AppDb _context;
        private readonly string _imageFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
        public GymProgramsController(AppDb context)
        {
            if (!Directory.Exists(_imageFolder))
            {
                Directory.CreateDirectory(_imageFolder);
            }
            _context = context;
        }

        // API method to create a gym program with an image upload
        [HttpPost("create with image")]
        public async Task<IActionResult> CreateProgramWithImage([FromForm] createProgramReq request, [FromForm] IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                return BadRequest("No image uploaded.");
            }

            // Save the image to the wwwroot/images folder
            var fileName = Path.GetFileName(image.FileName);
            var filePath = Path.Combine(_imageFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            // Now create the GymProgram and save the image path
            var gymProgram = new GymPrograms
            {
                Name = request.Name,
                Description = request.Description,
                Category = request.Category,
                Fees = request.Fees,
                ImagePath = $"/images/{fileName}" // Store the relative path
            };

           
            _context.gymprograms.Add(gymProgram);
            await _context.SaveChangesAsync();

            // Return the created program with the image path
            return Ok(gymProgram);
        }

        // GET: api/GymPrograms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GymPrograms>>> Getprograms()
        {
            return await _context.gymprograms.ToListAsync();
        }

        // GET: api/GymPrograms/5
        [HttpGet("get by id{id}")]
        public async Task<ActionResult<GymPrograms>> GetGymPrograms(int id)
        {
            var gymPrograms = await _context.gymprograms.FindAsync(id);

            if (gymPrograms == null)
            {
                return NotFound();
            }
            return gymPrograms;
        }


        // PUT: api/GymPrograms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGymPrograms(int id, GymPrograms gymPrograms)
        {
            if (id != gymPrograms.Id)
            {
                return BadRequest();
            }

            _context.Entry(gymPrograms).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GymProgramsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/GymPrograms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GymPrograms>> PostGymPrograms(createProgramReq programReq)
        {  
            var gymPrograms=new GymPrograms();
            
            gymPrograms.Name = programReq.Name;
            gymPrograms.Description = programReq.Description;
            gymPrograms.Category= programReq.Category;
            gymPrograms.Fees  = programReq.Fees;
          
            _context.gymprograms.Add(gymPrograms);

            
          

            return CreatedAtAction("GetGymPrograms", new { id = gymPrograms.Id }, gymPrograms);
        }
        // POST: api/GymProgram/upload


        // GET: api/Product/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<GymPrograms>> GetGymProgramById(int id)
        {
            var gymProgram = await _context.gymprograms.FindAsync(id);

            if (gymProgram == null)
            {
                return NotFound();
            }

            return Ok(gymProgram);
        }



        private bool IsValid(IFormFile file)
        {
            List<string> validFormats = new List<string>() {".jpg",".png",".jpeg" };
            var extention= "." + file.Name.Split('.')[file.FileName.Split('.').Length-1];
            return validFormats.Contains(extention); 
        }




        // DELETE: api/GymPrograms/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGymPrograms(int id)
        {
            var gymPrograms = await _context.gymprograms.FindAsync(id);
            if (gymPrograms == null)
            {
                return NotFound();
            }

            _context.gymprograms.Remove(gymPrograms);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GymProgramsExists(int id)
        {
            return _context.gymprograms.Any(e => e.Id == id);
        }
    }
}
