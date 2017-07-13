using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Knowlead.DomainModel.NotificationModels;
using Knowlead.DTO.NotificationModels;

namespace Knowlead.Services.Interfaces
{
    public interface INotificationServices
    {
        Task SendNotification (Notification notification);
        Task SendNotifications (List<Notification> notifications);
        Task SendNotificationEmails();
        Task<List<Notification>> GetPagedList(Guid applicationUserId, int offset, int numItems);
        Task MarkAsRead(Guid notificationId, Guid applicationUserId);
        Task MarkAllAsRead(Guid applicationUserId);
        Task DisplayNotification(Notification notification);
        Task<NotificationSourceStats> GetNotificationStats(Guid applicationUserId);
    }
}
