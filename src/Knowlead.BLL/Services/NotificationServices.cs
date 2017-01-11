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

        public Task Notify (List<Guid> userIds, String notificationType, TimeSpan delay)
        {
            //Save to database
            var notifications = _notificationRepository.NewNotification(userIds, notificationType);
            //Fire using hangfire
            if(TimeSpan.Equals(delay, TimeSpan.Zero))
            {
                BackgroundJob.Enqueue(
                () => Console.WriteLine("Fire-and-forget!"));
            }
            else
            {
                BackgroundJob.Schedule(
                () => _notificationRepository.NewNotification(userIds, ""), delay);
            }

            //needs to receive all notification and send id or whole object of those notifications to hangfire, also if its instant notification
            //fire it imidietely with details from object, if its scheduled, read the details from database using id sent to the function
            return Task.CompletedTask;
        }

        public Task ScheduleNotifications(List<Notification> notifications)
        {
            foreach (var notification in notifications)
            {
                
            }
            return Task.CompletedTask;
        }
    }
}