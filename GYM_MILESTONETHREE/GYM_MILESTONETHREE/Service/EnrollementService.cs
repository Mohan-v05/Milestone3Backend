using GYM_MILESTONETHREE.DataBase;
using GYM_MILESTONETHREE.IRepository;
using GYM_MILESTONETHREE.IService;
using GYM_MILESTONETHREE.Models;
using GYM_MILESTONETHREE.RequestModels;
using GYM_MILESTONETHREE.ResponseModels;

namespace GYM_MILESTONETHREE.Service
{
    public class EnrollementService : IEnrollementService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGymProgramRepository _programRepository;
        public EnrollementService(IEnrollmentRepository enrollmentRepository,IUserRepository userRepository, IGymProgramRepository programRepository)
        {
            _enrollmentRepository = enrollmentRepository;
            _userRepository = userRepository;
            _programRepository = programRepository;
        }

        public async Task<EnrollementResponse> Addenrollments(NewEnrollementsReq enrollmentData)
        {
            var user =await _userRepository.GetUserByIdAsync(enrollmentData.userId);
          
            List<Enrollments> enrollments = new List<Enrollments>();
            enrollmentData.ProgramIds.ForEach(async programId =>
            {
              
                var Enrollment = new Enrollments()
                {
                    UserId = enrollmentData.userId,
                    GymProgramId = programId,
                    EnrolledDate = DateTime.Now,
                  
                };
                enrollments.Add(Enrollment);
            });


            var data = await _enrollmentRepository.AddEnrollmentsAsync(enrollments);
            var Price = await CalculateFees(enrollmentData.userId);
            //user Table update with fee
            user.Fees = Price;
            var msg=_userRepository.updateUser(user);


            var enollmenetresponse = new EnrollementResponse()
            {
                userId = enrollmentData.userId,
                EnrolledPrice = Price,
                EnrolledDate = DateTime.Now,
                EnrolledProgramsId = enrollmentData.ProgramIds
            };
            return enollmenetresponse;

        }

        public async Task<Decimal> CalculateFees(int UserId)
        {
            Decimal EnrolledPrice = 0;
            var programs = await _enrollmentRepository.GetProgramsByUserIDAsync(UserId);
            programs.ForEach(program =>
            {
                EnrolledPrice = EnrolledPrice + program.Fees;
            });
            return EnrolledPrice;
        }

        public async Task<List<GymPrograms>> GetProgramsForMemberAsync(int UserId)
        {
            var programs = await _enrollmentRepository.GetProgramsByUserIDAsync(UserId);
            return programs;
        }

    }
}
