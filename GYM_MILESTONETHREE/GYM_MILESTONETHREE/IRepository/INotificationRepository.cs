using GYM_MILESTONETHREE.Controllers;
using GYM_MILESTONETHREE.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Notification = GYM_MILESTONETHREE.Models.Notification;

namespace GYM_MILESTONETHREE.IRepository
{
    public interface INotificationRepository
    {
        Task<Notification> AddNotificationAsync(Notification notification);
        Task<Notification> GetNotificationWithId(Guid notificationid);
        Task<Notification> markAsRead(Notification data);
        Task<Notification>  Delete(Notification data);
    }
}
