using GYM_MILESTONETHREE.IService;
using GYM_MILESTONETHREE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GYM_MILESTONETHREE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPatch]
        public async Task<IActionResult> markAsRead(Guid id)
        {
            var data = await _notificationService.markAsReadAsync(id);
            return Ok(data);

        }

          [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveNotificationAsync(Guid id)
        {
            var data = await _notificationService.removeNotificationAsync(id);
            return Ok(data);
        }
    }
}
