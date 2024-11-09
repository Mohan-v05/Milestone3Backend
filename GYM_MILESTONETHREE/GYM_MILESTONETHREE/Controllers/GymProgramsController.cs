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
            return await _context.programs.ToListAsync();
        }

        // GET: api/GymPrograms/5
        [HttpGet("get by id{id}")]
        public async Task<ActionResult<GymPrograms>> GetGymPrograms(int id)
        {
            var gymPrograms = await _context.programs.FindAsync(id);

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
        public async Task<ActionResult<GymPrograms>> PostGymPrograms(GymPrograms gymPrograms)
        {  
            _context.programs.Add(gymPrograms);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGymPrograms", new { id = gymPrograms.Id }, gymPrograms);
        }
        // POST: api/GymProgram/upload


        // GET: api/Product/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<GymPrograms>> GetGymProgramById(int id)
        {
            var gymProgram = await _context.programs.FindAsync(id);

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
            var gymPrograms = await _context.programs.FindAsync(id);
            if (gymPrograms == null)
            {
                return NotFound();
            }

            _context.programs.Remove(gymPrograms);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GymProgramsExists(int id)
        {
            return _context.programs.Any(e => e.Id == id);
        }
    }
}
