using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Knowlead.DomainModel.NotificationModels;
using Knowlead.DTO.NotificationModels;

namespace Knowlead.BLL.Repositories.Interfaces
{
    public interface INotificationRepository
    {
        Task<Notification> Get(Guid notificationId);
        Task<List<Notification>> GetPagedList(Guid applicationUserId, int offset, int numItems);
        Task<List<Notification>> GetAllWhere(Expression<Func<Notification, bool>> condition);
        void Add(Notification notification);
        void Update(Notification notification);
        Task Commit();
        Task<NotificationSourceStats> GetNotificationStats(Guid applicationUserId);
        Task SendNotificationEmails();
    }
}