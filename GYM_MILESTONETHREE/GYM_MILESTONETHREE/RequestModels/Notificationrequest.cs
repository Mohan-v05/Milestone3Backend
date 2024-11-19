using GYM_MILESTONETHREE.Models;

namespace GYM_MILESTONETHREE.RequestModels
{
    public class Notificationrequest
    {
        public string Title { get; set; }

        public string Message { get; set; }

        public Users User { get; set; }

        public int UserId { get; set; }

        //User Received // Soft Deleted=flase
        public Boolean status { get; set; }

        //Read or not 
        public Boolean isRead { get; set; }
    }
}
