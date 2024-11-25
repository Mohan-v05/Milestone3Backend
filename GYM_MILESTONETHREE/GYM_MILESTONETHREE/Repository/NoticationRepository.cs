using GYM_MILESTONETHREE.DataBase;
using GYM_MILESTONETHREE.IRepository;
using GYM_MILESTONETHREE.Models;

namespace GYM_MILESTONETHREE.Repository
{
    public class NoticationRepository: INotificationRepository
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
        public async Task<Notification> GetNotificationWithId(Guid guid)
        {
            var data = await _context.notification.FindAsync(guid);
            return data;
        }

        public async Task<Notification> markAsRead(Notification data)
        {
            var response =  _context.notification.Update(data);
            await _context.SaveChangesAsync();
            return response.Entity;
        }

        public async Task<Notification> Delete(Notification data)
        {
            var res = _context.notification.Remove(data);
            await _context.SaveChangesAsync();
            return res.Entity;

        }
    }
}
