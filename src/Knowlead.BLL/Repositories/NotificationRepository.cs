using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.Common.Exceptions;
using Knowlead.DAL;
using Knowlead.DomainModel.NotificationModels;
using Microsoft.EntityFrameworkCore;
using static Knowlead.Common.Constants;

namespace Knowlead.BLL.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Notification> Get(Guid notificationId)
        {
            return await _context.Notifications.Where(x => x.NotificationId.Equals(notificationId)).FirstOrDefaultAsync();
        }

        public async Task<List<Notification>> GetAllWhere(Expression<Func<Notification, bool>> condition)
        {
            return await _context.Notifications.Where(condition).ToListAsync();
        }

        public async Task<List<Notification>> GetPagedList(Guid applicationUserId, int offset, int numItems)
        {
            return await _context.Notifications.Where(x => x.ForApplicationUserId.Equals(applicationUserId))
                                                .Where(x => x.ScheduledAt <= DateTime.UtcNow)
                                                .OrderByDescending(x => x.ScheduledAt)
                                                .Skip(offset).Take(numItems).ToListAsync();
        }

        public async Task<List<Notification>> InsertNotification(List<Guid> userIds, String notificationType, DateTime scheduledAt)
        {
            var notifications = new List<Notification>();
            foreach (var userId in userIds)
            {
                var notification = new Notification(userId, notificationType, scheduledAt);
                notification.FromApplicationUserId = userId;
                notifications.Add(notification);
                _context.Notifications.Add(notification);
            }

            await Commit();
            
            return notifications;
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
    }
}