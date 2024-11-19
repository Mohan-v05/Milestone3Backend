using System.ComponentModel.DataAnnotations;

namespace GYM_MILESTONETHREE.Models
{
    public class Notification
    {
        public Guid Id { get; set; }

        public string? Title { get; set; }

        public string? Message { get; set; }

        public Users? User { get; set; }

        public int UserId {  get; set; }

        //Deleted or not
        public Boolean status {  get; set; }
        
        //Read or not 
        public Boolean isRead {  get; set; }
    }
}
