using Azure;
using GYM_MILESTONETHREE.DataBase;
using GYM_MILESTONETHREE.IRepository;
using GYM_MILESTONETHREE.IService;
using GYM_MILESTONETHREE.Models;
using GYM_MILESTONETHREE.ResponseModels;

namespace GYM_MILESTONETHREE.Service
{
    public class GymProgramService : IGymProgramService
    {
        private readonly IGymProgramRepository _repository;
        private readonly IEnrollmentRepository _enrollmentRepository;

        public GymProgramService(IGymProgramRepository repository, IEnrollmentRepository enrollmentRepository)
        {
            _repository = repository;
            _enrollmentRepository = enrollmentRepository;
        }

        public async Task<GymPrograms> GetGymProgramsbyIdAsync(int id)
        {
           
           var data= await _repository.GetGymProgramsbyIdAsync(id);
           return data;
        }


        public async Task<List<GymPrograms>> GetAllGymProgramsAsync()
        {
            var data = await _repository.GetAllGymProgramsAsync();

            if (data == null)
            {
                throw new Exception("No gym programs found.");
            }

            // Use Task.WhenAll to await all asynchronous tasks in the Select

            return data;
        }

        private async Task<int> CalculateLength(int id)
        {
            var users = await _enrollmentRepository.GetUsersByProgramIDAsync(id);
            return users.Count(); // This will return an int
        }



    }


}
