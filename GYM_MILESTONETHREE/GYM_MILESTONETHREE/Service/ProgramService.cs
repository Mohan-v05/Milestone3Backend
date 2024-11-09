using GYM_MILESTONETHREE.DataBase;
using GYM_MILESTONETHREE.IRepository;
using GYM_MILESTONETHREE.IService;

namespace GYM_MILESTONETHREE.Service
{
    public class ProgramService : IProgramService
    {
        private readonly IGymProgramRepository _repository;

        public ProgramService(IGymProgramRepository repository)
        {
            _repository = repository;
        }
    }
}
