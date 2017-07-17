using Knowlead.BLL.Emails.Interfaces;

namespace Knowlead.BLL.Emails
{
    public class RegistrationEmail : IEmail
    {
        public string TemplateFilename{ get; private set; }
        public string RegistrationLink { get; set; }

        public RegistrationEmail()
        {
            TemplateFilename = "registration.html";
        }

        public string FillTemplate(string template)
        {
            return template.Replace("{{RegistrationLink}}", RegistrationLink);
        }
    }
}