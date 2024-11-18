using GYM_MILESTONETHREE.DataBase;
using GYM_MILESTONETHREE.IRepository;
using GYM_MILESTONETHREE.Models;
using Microsoft.EntityFrameworkCore;

namespace GYM_MILESTONETHREE.Repository
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly AppDb _context;

        public EnrollmentRepository(AppDb context)
        {
            _context = context;
        }

        public async Task<List<Enrollments>> AddEnrollmentsAsync(List<Enrollments> enrollments)
        {

            if (enrollments == null || !enrollments.Any())
            {
                throw new ArgumentException("Enrollments list cannot be null or empty.");
            }

            try
            {
                await _context.enrollments.AddRangeAsync(enrollments);
                await _context.SaveChangesAsync();
                return enrollments;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding enrollments.", ex);
            }
        }


        public async Task<List<GymPrograms>> GetProgramsByUserIDAsync(int userID)
        {
            if (userID <= 0)
            {
                throw new ArgumentException("Invalid user ID. It must be greater than 0.", nameof(userID));
            }

            try
            {
                var programs = await _context.enrollments.
                     Where(e => e.UserId == userID)
                    .Select(e => e.GymProgram)  // Assuming GymProgram is a navigation property
                    .ToListAsync();
                if ( programs == null)
                {
                    throw new Exception("User dont have any enrollments");
                }
                    

                return programs;
            }
            catch (Exception ex)
            {
               
                throw new ApplicationException($"An error occurd while Getting programs {userID}.", ex);
            }
        }
        public async Task<List<Users>> GetUsersByProgramIDAsync(int programId)
        {
            if (programId <= 0)
            {
                throw new ArgumentException("Invalid user ID. It must be greater than 0.", nameof(programId));
            }

            try
            {
                var users = await _context.enrollments.
                     Where(e => e.GymProgramId == programId)
                    .Select(e => e.User)  // Assuming GymProgram is a navigation property
                    .ToListAsync();
                if (users == null)
                {
                    throw new Exception("program dont have any enrollments");
                }
                return users;
            }
            catch (Exception ex)
            {

                throw new ApplicationException($"An error occurd while Getting programs {programId}.", ex);
            }
        }
    }
}
