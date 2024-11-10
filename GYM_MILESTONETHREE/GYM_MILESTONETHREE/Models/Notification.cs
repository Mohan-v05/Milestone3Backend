using System.ComponentModel.DataAnnotations;

namespace GYM_MILESTONETHREE.Models
{
    public class Notification
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public Users Users { get; set; }

        public int userId {  get; set; }

        public Boolean status {  get; set; }
        public Boolean isRead {  get; set; }
    }
}
