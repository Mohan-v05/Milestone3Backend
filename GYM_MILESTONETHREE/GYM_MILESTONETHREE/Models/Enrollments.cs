using Microsoft.VisualBasic;

namespace GYM_MILESTONETHREE.Models
{
    public class Enrollments
    {   
        public Guid Id { get; set; }
        public Users User { get; set; }

        public int UserId { get; set; }

        public GymPrograms GymProgram { get; set; }

        public int GymProgramId { get; set; }

        public DateTime EnrolledDate {  get; set; }

    }
}
