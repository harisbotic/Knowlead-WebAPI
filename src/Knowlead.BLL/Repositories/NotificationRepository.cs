using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Knowlead.BLL.Emails;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.Common.Exceptions;
using Knowlead.DAL;
using Knowlead.DomainModel.NotificationModels;
using Knowlead.DomainModel.UserModels;
using Knowlead.DTO.NotificationModels;
using Knowlead.Services;
using Knowlead.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static Knowlead.Common.Constants;

namespace Knowlead.BLL.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private ApplicationDbContext _context;
        private IAccountRepository _accountRepository;
        private MessageServices _messageServices;

        public NotificationRepository(ApplicationDbContext context, IAccountRepository accountRepository, MessageServices messageServices)
        {
            _context = context;
            _accountRepository = accountRepository;
            _messageServices = messageServices;
        }

        public async Task<Notification> Get(Guid notificationId)
        {
            return await _context.Notifications.Where(x => x.NotificationId.Equals(notificationId)).FirstOrDefaultAsync();
        }

        public async Task<List<Notification>> GetAllWhere(Expression<Func<Notification, bool>> condition)
        {
            return await _context.Notifications.Where(condition).ToListAsync();
        }

        public async Task<Dictionary<Guid, List<Notification>>> GetAllUnread()
        {
            return await _context.Notifications.Where(n => (!n.SeenAt.HasValue && !n.IsEmailSent))
                .GroupBy(n => n.ForApplicationUserId)
                .ToDictionaryAsync(group => group.Key, group => group.ToList());
        }

        public async Task<List<Notification>> GetPagedList(Guid applicationUserId, int offset, int numItems)
        {
            return await _context.Notifications.Where(x => x.ForApplicationUserId.Equals(applicationUserId))
                                                .Where(x => x.ScheduledAt <= DateTime.UtcNow)
                                                .OrderByDescending(x => x.ScheduledAt)
                                                .Skip(offset).Take(numItems).ToListAsync();
        }

        public void Add(Notification notification)
        {
            _context.Notifications.Add(notification);
        }

        public void Update(Notification notification)
        {
            _context.Notifications.Update(notification);
        }

        public async Task Commit()
        {
            var success = await _context.SaveChangesAsync() > 0;
            if(!success)
                throw new ErrorModelException(ErrorCodes.DatabaseError); //No changed were made to entity
        }

        public async Task<NotificationSourceStats> GetNotificationStats(Guid applicationUserId)
        {
            return new NotificationSourceStats(){
                Unread = await _context.Notifications.Where(x => x.ForApplicationUserId == applicationUserId && !x.SeenAt.HasValue).CountAsync(),
                Total = await _context.Notifications.Where(x => x.ForApplicationUserId == applicationUserId).CountAsync()
            };
        }

        
        public async Task SendNotificationEmails()
        {
            Dictionary<Guid, List<Notification>> notifications = await GetAllUnread();
            foreach(Guid userGuid in notifications.Keys)
            {
                NotificationEmail email = new NotificationEmail();
                email.Notifications = notifications[userGuid];
                
                ApplicationUser user = await _accountRepository.GetApplicationUserById(userGuid);
                if(await _messageServices.SendEmailAsync(user.Email, "Notifications", email)) 
                {
                    foreach(Notification n in notifications[userGuid])
                    {
                        n.IsEmailSent = true;
                        Update(n);
                    }
                }
            }
            
            try
            {
                await Commit();
            }
            catch { }
        }
    }
}