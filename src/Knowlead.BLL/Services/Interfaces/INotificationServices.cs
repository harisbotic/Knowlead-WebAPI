using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Knowlead.Services.Interfaces
{
    public interface INotificationServices
    {
        Task Notify (List<Guid> userIds, String notificationType, TimeSpan delay);
        // Task ScheduleNotification(List<Notification> notifications);
    }
}
