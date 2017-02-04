using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Knowlead.DomainModel.NotificationModels;

namespace Knowlead.Services.Interfaces
{
    public interface INotificationServices
    {
        Task NewNotification (Guid applicationUserId, String notificationType, DateTime scheduleAt);
        Task NewNotification (List<Guid> applicationUserIds, String notificationType, DateTime scheduleAt);
        Task DisplayNotifications(List<Notification> notifications, bool now);
    }
}
