using GYM_MILESTONETHREE.IRepository;
using GYM_MILESTONETHREE.IService;
using GYM_MILESTONETHREE.Models;
using GYM_MILESTONETHREE.RequestModels;

namespace GYM_MILESTONETHREE.Service
{
    public class NotificationService: INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task <bool> AddNotificationAsync(Notificationrequest notificationrequest)
        {
            if (notificationrequest != null)
            {
                var notification = new Notification
                {
                    UserId = notificationrequest.UserId,
                    Title = notificationrequest.Title,
                    Message = notificationrequest.Message,
                    isRead = notificationrequest.isRead,
                    status = notificationrequest.status,

                };
                var data = _notificationRepository.AddNotificationAsync(notification);
                return true;
            }
            else
            {
                return false;
            }
          
        }
        public async Task<Notification> markAsReadAsync(Guid notificationid)
        {
            var data = await _notificationRepository.getNotificationByIdAsync(notificationid);
            if (data!=null)
            {
                data.isRead = true;
                var response = await _notificationRepository.updateNotification(data);
                return response;
            }
            else
            {
                throw new Exception("Notification Not Found");
            }


        }

        public async Task<Notification> removeNotificationAsync(Guid notificationid)
        {

            var data =await _notificationRepository.getNotificationByIdAsync(notificationid);
            if (data != null)
            {
                data.status = false;
                var response = await _notificationRepository.updateNotification(data);
                return response;
            }
            else
            {
                throw new Exception("Notification Not Found");
            }
        }
        

    }
}
