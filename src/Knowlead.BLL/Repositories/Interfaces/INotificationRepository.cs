using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Knowlead.DomainModel.NotificationModels;

namespace Knowlead.BLL.Repositories.Interfaces
{
    public interface INotificationRepository
    {
        Task<List<Notification>> NewNotification(List<Guid> userIds, String notificationType, DateTime scheduledAt);
    }
}