using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Knowlead.DomainModel.NotificationModels;

namespace Knowlead.Services.Interfaces
{
    public interface INotificationServices
    {
        Task Notify (Guid userId, String notificationType, DateTime ScheduleNotification);
        Task NotifyMore (List<Guid> userIds, String notificationType, DateTime ScheduleNotification);
        Task PublishNotifications(List<Notification> notifications, bool now);
    }
}
