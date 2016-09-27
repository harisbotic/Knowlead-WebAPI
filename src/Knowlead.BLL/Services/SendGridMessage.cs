using System.Collections.Generic;

namespace Knowlead.Services
{
    public class SendGridMessage
    {
        public const string TYPE_TEXT = "text";
        public const string TYPE_HTML = "text/html";

        public List<SendGridPersonalization> personalizations { get; set; }
        public SendGridEmail from { get; set; }
        public List<SendGridContent> content { get; set; }

        public SendGridMessage() { }

        public SendGridMessage(SendGridEmail to, string subject, SendGridEmail from, string message, string type = TYPE_HTML)
        {
            personalizations = new List<SendGridPersonalization> { new SendGridPersonalization { to = new List<SendGridEmail> { to }, subject = subject } };
            this.from = from;
            content = new List<SendGridContent> { new SendGridContent(type, message) };
        }
    }

    public class SendGridPersonalization
    {
        public List<SendGridEmail> to { get; set; }
        public string subject { get; set; }
    }

    public class SendGridEmail
    {
        public string email { get; set; }
        public string name { get; set; }

        public SendGridEmail() { }

        public SendGridEmail(string email, string name = null)
        {
            this.email = email;
            this.name = name;
        }
    }

    public class SendGridContent
    {
        public string type { get; set; }
        public string value { get; set; }

        public SendGridContent() { }

        public SendGridContent(string type, string content)
        {
            this.type = type;
            value = content;
        }
    }
}