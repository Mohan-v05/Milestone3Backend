using GYM_MILESTONETHREE.DataBase;
using GYM_MILESTONETHREE.IRepository;
using GYM_MILESTONETHREE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GYM_MILESTONETHREE.Repository
{
    public class GymProgramsRepository: IGymProgramRepository
    {
        private readonly AppDb _context;

        public GymProgramsRepository(AppDb context)
        {
            _context = context;
        }
        public async Task<GymPrograms> GetGymProgramsbyIdAsync(int id)
        {
            var gymProgram = await _context.gymprograms.FirstOrDefaultAsync( p => p.Id ==id);
            return gymProgram;
         
        }

        public async Task<List<GymPrograms>> GetAllGymProgramsAsync()
        {
            var programs = await _context.gymprograms.ToListAsync();
            return programs;
        }
    }
}
