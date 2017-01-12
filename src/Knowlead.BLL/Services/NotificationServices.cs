using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hangfire;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.DomainModel.NotificationModels;
using Knowlead.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;

namespace Knowlead.Services
{
    public class NotificationServices<THub> : INotificationServices where THub : class
    {
        private readonly IConfigurationRoot _config;
        private readonly IHubContext<THub> _hubContext;
        private readonly INotificationRepository _notificationRepository;
        public NotificationServices(IConfigurationRoot config, IHubContext<THub> hubContext,
                                    INotificationRepository notificationRepository)
        {
            _config = config;
            _hubContext = hubContext;
            _notificationRepository = notificationRepository;
        }

        public async Task Notify (Guid userId, String notificationType, DateTime scheduleAt)
        {
            await NotifyMore(new List<Guid>(){userId}, notificationType, scheduleAt);
        }

        public async Task NotifyMore (List<Guid> userIds, String notificationType, DateTime scheduleAt)
        {
            var notifications = await _notificationRepository.NewNotification(userIds, notificationType, scheduleAt);

            if(scheduleAt.Subtract(DateTime.UtcNow).TotalSeconds < 30)
            {
                await PublishNotifications(notifications, true);
            }
            else
            {
                BackgroundJob.Schedule<INotificationServices>(
                (x) => x.PublishNotifications(notifications, false), scheduleAt.Subtract(DateTime.UtcNow));
            }
        }

        public Task PublishNotifications(List<Notification> notifications, bool now = false)
        {
            foreach (var notification in notifications)
            {
                //if(now || notification.ScheduledAt.CompareTo(DateTime.UtcNow) < 0)
                _hubContext.Clients.User(notification.ForApplicationUserId.ToString()).InvokeAsync("newNotification", notification);  
            }

            return Task.CompletedTask;
        }
    }
}