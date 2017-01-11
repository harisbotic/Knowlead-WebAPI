using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.Common.Exceptions;
using Knowlead.DAL;
using Knowlead.DomainModel.NotificationModels;
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

        public async Task<List<Notification>> NewNotification(List<Guid> userIds, String notificationType)
        {
            var notifications = new List<Notification>();
            foreach (var userId in userIds)
            {
                var notification = new Notification(userId, notificationType);
                notifications.Add(notification);
                _context.Notifications.Add(notification);
            }

            await SaveChangesAsync();
            
            return notifications;
        }

        private async Task SaveChangesAsync()
        {
            var success = await _context.SaveChangesAsync() > 0;
            if(!success)
                throw new ErrorModelException(ErrorCodes.DatabaseError); //No changed were made to entity
        }
    }
}