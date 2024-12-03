namespace GYM_MILESTONETHREE.RequestModels
{
    public class UpdateProgramReq
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public decimal? Fees { get; set; }
        public IFormFile? image { get; set; }
    }
}
