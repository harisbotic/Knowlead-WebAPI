using System.Collections.Generic;
using Knowlead.BLL.Emails.Interfaces;
using Knowlead.DomainModel.NotificationModels;

namespace Knowlead.BLL.Emails
{
    public class NotificationEmail : IEmail
    {
        public string TemplateFilename { get; } = "notification.html";
        public List<Notification> Notifications;

        public string FillTemplate(string template)
        {
            return template.Replace("{{text}}", "You have new notification");
        }
    }
}