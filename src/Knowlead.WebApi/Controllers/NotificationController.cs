using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Knowlead.Common.Exceptions;
using Knowlead.Common.HttpRequestItems;
using Knowlead.DomainModel.NotificationModels;
using Knowlead.DTO.NotificationModels;
using Knowlead.DTO.ResponseModels;
using Knowlead.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Knowlead.Common.Constants;

namespace Knowlead.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = Policies.RegisteredUser)]
    public class NotificationController : Controller
    {
        private readonly INotificationServices _notificationServices;
        private readonly Auth _auth;

        public NotificationController(INotificationServices notificationServices, Auth auth)
        {
            _notificationServices = notificationServices;
            _auth = auth;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetList(int offset = 0, int numItems = 10) //TODO: limit those numbers
        {
            var applicationUserId = _auth.GetUserId();

            var notifications = await _notificationServices.GetPagedList(applicationUserId, offset, numItems);

            if(notifications == null)
                throw new ErrorModelException(ErrorCodes.EntityNotFound, nameof(Notification));

            return Ok(new ResponseModel()
            {
                Object = AutoMapper.Mapper.Map<List<NotificationModel>>(notifications)
            });
        }

        [HttpPost("markAsRead")]
        public async Task<IActionResult> MarkAllAsRead()
        {
            var applicationUserId = _auth.GetUserId();

            await _notificationServices.MarkAllAsRead(applicationUserId);

            return Ok(new ResponseModel()
            {
            });
        }

        [HttpPost("markAsRead/{notificationId}")]
        public async Task<IActionResult> MarkAsRead(Guid notificationId)
        {
            var applicationUserId = _auth.GetUserId();

            await _notificationServices.MarkAsRead(notificationId, applicationUserId);

            return Ok(new ResponseModel()
            {
            });
        }
    }
}