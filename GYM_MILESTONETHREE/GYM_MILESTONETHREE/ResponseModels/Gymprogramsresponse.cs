using GYM_MILESTONETHREE.Models;

namespace GYM_MILESTONETHREE.ResponseModels
{
    public class Gymprogramsresponse
    {
        public int Id { get; set; }

       
        public string Name { get; set; }

        public string? Description { get; set; }

        public string? Category { get; set; }

        public decimal Fees { get; set; }

        public string? ImagePath { get; set; }

        public ICollection<Enrollments> enrollments { get; set; }

        public int NoofEnrollment {  get; set; }
    }
}
