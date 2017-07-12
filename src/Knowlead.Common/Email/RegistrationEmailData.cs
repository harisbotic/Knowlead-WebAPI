using Knowlead.Common.Email.Interfaces;

namespace Knowlead.Common.Email
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