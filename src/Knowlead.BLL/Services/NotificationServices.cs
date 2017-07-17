using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Hangfire;
using Knowlead.BLL.Emails;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.Common.Exceptions;
using Knowlead.DomainModel.NotificationModels;
using Knowlead.DomainModel.UserModels;
using Knowlead.DTO.NotificationModels;
using Knowlead.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using static Knowlead.Common.Constants;

namespace Knowlead.Services
{
    public class NotificationServices<THub> : INotificationServices where THub : Hub
    {
        private readonly IHubContext<THub> _hubContext;
        private readonly INotificationRepository _notificationRepository;
        private readonly MessageServices _messageServices;
        private readonly IAccountRepository _accountRepository;
        public NotificationServices(IHubContext<THub> hubContext, INotificationRepository notificationRepository, MessageServices messageServices, IAccountRepository accountRepository)
        {
            _hubContext = hubContext;
            _notificationRepository = notificationRepository;
            _messageServices = messageServices;
            _accountRepository = accountRepository;
        }

        public async Task<Notification> Get(Guid notificationId, Guid applicationUserId)
        {
            var notification = await _notificationRepository.Get(notificationId);

            if(notification == null)
                throw new ErrorModelException(ErrorCodes.EntityNotFound, nameof(Notification));

            if(!notification.ForApplicationUserId.Equals(applicationUserId))
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
        
        public async Task SendNotification(Notification notification)
        {
            await SendNotifications(new List<Notification>(){notification});
        }

        public async Task SendNotifications (List<Notification> notifications)
        {
            foreach (var notification in notifications)
                _notificationRepository.Add(notification);
            
            await _notificationRepository.Commit(); //Saving to db

            foreach (var notification in notifications)
            {
                var scheduleAt = notification.ScheduledAt;

                if(scheduleAt.Subtract(DateTime.UtcNow).TotalSeconds < 30)
                {
                    await DisplayNotification(notification);
                }
                else
                {
                    BackgroundJob.Schedule<INotificationServices>(
                    (x) => x.DisplayNotification(notification), scheduleAt.Subtract(DateTime.UtcNow));
                }
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
        public async Task DisplayNotification(Notification notification)
        {
            var json = JsonConvert.SerializeObject(Mapper.Map<NotificationModel>(notification), new JsonSerializerSettings 
            { 
                ContractResolver = new CamelCasePropertyNamesContractResolver() 
            });

            await _hubContext.Clients.User(notification.ForApplicationUserId.ToString())
                                .InvokeAsync(WebClientFuncNames.DisplayNotification, json);
        }

        public async Task<NotificationSourceStats> GetNotificationStats(Guid applicationUserId)
        {
            return await _notificationRepository.GetNotificationStats(applicationUserId);
        }

    }
}