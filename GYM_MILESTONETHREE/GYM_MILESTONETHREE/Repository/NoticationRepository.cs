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

    }
}
