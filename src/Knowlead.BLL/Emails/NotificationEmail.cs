using System;
using System.Collections.Generic;
using Knowlead.BLL.Emails.Interfaces;
using static Knowlead.Common.Constants.NotificationTypes;
using Knowlead.DomainModel.NotificationModels;

namespace Knowlead.BLL.Emails
{
    public class NotificationEmail : IEmail
    {
        public static readonly List<String> ALLOWED_NOTIFICATION_TYPES = new List<String>{
            NewP2PComment, P2POfferAccepted, P2PScheduled, FriendshipRequest, FriendshipAccepted};
        public string TemplateFilename { get; } = "notification.html";
        public List<Notification> Notifications;

        public string FillTemplate(string template)
        {
            return template.Replace("{{text}}", "You have new notification");
        }
    }
}