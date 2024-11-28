using GYM_MILESTONETHREE.Models;

namespace GYM_MILESTONETHREE.IRepository
{
    public interface INotificationRepository
    {
        Task<Notification> AddNotificationAsync(Notification notification);
        Task<Notification> getNotificationByIdAsync(Guid notificationid);
        Task<Notification> updateNotification(Notification notification);

    }
}
