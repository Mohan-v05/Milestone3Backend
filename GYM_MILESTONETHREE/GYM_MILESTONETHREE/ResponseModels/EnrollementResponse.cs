namespace GYM_MILESTONETHREE.ResponseModels
{
    public class EnrollementResponse
    {
 
        public int userId { get; set; }
        public List<int> EnrolledProgramsId {  get; set; }
        public decimal EnrolledPrice {  get; set; }

        public DateTime EnrolledDate { get; set; }
    }
}
