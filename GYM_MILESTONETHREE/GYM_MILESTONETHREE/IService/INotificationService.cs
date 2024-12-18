﻿using GYM_MILESTONETHREE.Models;
using GYM_MILESTONETHREE.RequestModels;

namespace GYM_MILESTONETHREE.IService
{
    public interface INotificationService
    {
        Task<bool> AddNotificationAsync(Notificationrequest notificationrequest);

        Task<Notification> markAsReadAsync(Guid notificationid);

        Task<Notification> removeNotificationAsync(Guid notificationid);

    }
}
