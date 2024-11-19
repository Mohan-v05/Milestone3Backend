using GYM_MILESTONETHREE.Models;

namespace GYM_MILESTONETHREE.IRepository
{
    public interface INotificationRepository
    {
        Task<Notification> AddNotificationAsync(Notification notification);
    }
}
