using GYM_MILESTONETHREE.Enums;

namespace GYM_MILESTONETHREE.Models
{
    public class EmailTemplates
    {
        public Guid Id { get; set; }
        public EmailTypes emailTypes { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

    }
}
