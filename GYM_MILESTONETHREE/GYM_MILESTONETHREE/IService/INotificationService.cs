using GYM_MILESTONETHREE.RequestModels;

namespace GYM_MILESTONETHREE.IService
{
    public interface INotificationService
    {
        Task<bool> AddNotificationAsync(Notificationrequest notificationrequest);
    }
}
