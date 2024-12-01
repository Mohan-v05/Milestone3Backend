using GYM_MILESTONETHREE.DataBase;
using GYM_MILESTONETHREE.Enums;
using GYM_MILESTONETHREE.Models;
using Microsoft.EntityFrameworkCore;

namespace GYM_MILESTONETHREE.Repository
{
    public class SendMailRepository(AppDb _Context)
    {
        public async Task<EmailTemplates> GetTemplate(EmailTypes emailTypes)
        {
            var template = _Context.EmailTemplates.Where(x => x.emailTypes == emailTypes).FirstOrDefault();
            return template;
        }
    }
}
