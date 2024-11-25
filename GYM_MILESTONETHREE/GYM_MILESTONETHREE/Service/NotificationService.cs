using GYM_MILESTONETHREE.IRepository;
using GYM_MILESTONETHREE.IService;
using GYM_MILESTONETHREE.Models;
using GYM_MILESTONETHREE.RequestModels;
using Microsoft.Identity.Client;

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
        
        public async Task<Notification> markAsRead(Guid notificationid)
        {
            var data= await _notificationRepository.GetNotificationWithId(notificationid);

           if (data != null)
            {
                data.isRead = true;
               var data2= await _notificationRepository.markAsRead(data); 
                return data2;
            }
            else
            {
                throw new Exception("nNOtification not found");
            }
        }
        public async Task<Notification> RemoveNotificationAsync(Guid notificationid)
        {
            var data = await _notificationRepository.GetNotificationWithId(notificationid);

            if (data != null)
            {
                data.status = false;
                var data2 = await _notificationRepository.Delete(data);
                return data2;
            }
            else
            {
                throw new Exception("nNOtification not found");
            }
        }
    }
}
