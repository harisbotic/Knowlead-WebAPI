using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.Common.Exceptions;
using Knowlead.DomainModel.NotificationModels;
using Knowlead.DTO.NotificationModels;
using Knowlead.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using static Knowlead.Common.Constants;

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

        public async Task<Notification> Get(Guid notificationId, Guid applicationUserId)
        {
            var notification = await _notificationRepository.Get(notificationId);

            if(notification == null)
                throw new ErrorModelException(ErrorCodes.EntityNotFound, nameof(Notification));

            if(!notification.FromApplicationUserId.Equals(applicationUserId))
                throw new ErrorModelException(ErrorCodes.AuthorityError);

            return notification;
        }

        public async Task<List<Notification>> GetAllUnread(Guid applicationUserId)
        {
            var notifications = await _notificationRepository.GetAllWhere(x => x.ForApplicationUserId.Equals(applicationUserId));

            return notifications;
        }

        public async Task<List<Notification>> GetPagedList(Guid applicationUserId, int offset, int numItems)
        {
            var notifications = await _notificationRepository.GetPagedList(applicationUserId, offset, numItems);

            return notifications;
        }

        public async Task NewNotification(Guid applicationUserId, String notificationType, DateTime scheduleAt)
        {
            await NewNotification(new List<Guid>(){applicationUserId}, notificationType, scheduleAt);
        }

        public async Task NewNotification(List<Guid> applicationUserIds, String notificationType, DateTime scheduleAt)
        {
            var notifications = await _notificationRepository.InsertNotification(applicationUserIds, notificationType, scheduleAt);

            if(scheduleAt.Subtract(DateTime.UtcNow).TotalSeconds < 30)
            {
                await DisplayNotifications(notifications, true);
            }
            else
            {
                BackgroundJob.Schedule<INotificationServices>(
                (x) => x.DisplayNotifications(notifications, false), scheduleAt.Subtract(DateTime.UtcNow));
            }
        }

        public async Task MarkAsRead(Guid notificationId, Guid applicationUserId)
        {
            var notification = await Get(notificationId, applicationUserId);

            notification.SeenAt = DateTime.UtcNow;

            _notificationRepository.Update(notification);
            await _notificationRepository.Commit();
        }

        public async Task MarkAllAsRead(Guid applicationUserId)
        {
            var notifications = await GetAllUnread(applicationUserId);

            foreach (var notification in notifications)
            {
                notification.SeenAt = DateTime.UtcNow;
                _notificationRepository.Update(notification);
            }

            await _notificationRepository.Commit();
        }

        [AutomaticRetry(Attempts = 1, OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public Task DisplayNotifications(List<Notification> notifications, bool now = false)
        {
            foreach (var notification in notifications)
            {
                var json = JsonConvert.SerializeObject(Mapper.Map<NotificationModel>(notification), new JsonSerializerSettings 
                { 
                    ContractResolver = new CamelCasePropertyNamesContractResolver() 
                });

                _hubContext.Clients.User(notification.ForApplicationUserId.ToString())
                                    .InvokeAsync(WebClientFunctionNames.DisplayNotification, json);  
            }

            return Task.CompletedTask;
        }
    }
}