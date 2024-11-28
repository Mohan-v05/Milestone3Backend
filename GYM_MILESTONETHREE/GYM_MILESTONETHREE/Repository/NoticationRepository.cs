using GYM_MILESTONETHREE.DataBase;
using GYM_MILESTONETHREE.IRepository;
using GYM_MILESTONETHREE.Models;
using Microsoft.EntityFrameworkCore;

namespace GYM_MILESTONETHREE.Repository
{
    public class NoticationRepository : INotificationRepository
    {
        private readonly AppDb _context;

        public NoticationRepository(AppDb appDb)
        {
            _context = appDb;
        }
        public async Task<Notification> AddNotificationAsync(Notification notification)
        {
            var data = await _context.AddAsync(notification);
            await _context.SaveChangesAsync();
            return data.Entity;
        }


        public async Task<Notification> getNotificationByIdAsync(Guid notificationid)
        {
            var data = await _context.notification.FindAsync(notificationid);
            await _context.SaveChangesAsync();
            return data;
         }

       public  async Task<Notification> updateNotification(Notification notification)
        {
            var response =  _context.notification.Update(notification);
            await _context.SaveChangesAsync();
            return response.Entity;
        }
    }
}
