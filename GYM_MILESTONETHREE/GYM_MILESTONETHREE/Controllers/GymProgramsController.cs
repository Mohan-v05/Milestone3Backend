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

namespace GYM_MILESTONETHREE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GymProgramsController : ControllerBase
    {
        private readonly AppDb _context;
        private readonly string _imageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
        public GymProgramsController(AppDb context)
        {
            _context = context;
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
            if (file == null || file.Length == 0)
            {
                return BadRequest("Image is not Proper");
            }
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);

            var image = new ImageModel
            {
                Name = file.FileName,
                Data = ms.ToArray(),
                ContentType = file.ContentType,

            };
            _context.Images.Add(image);

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
