using Knowlead.BLL.Emails.Interfaces;

namespace Knowlead.BLL.Emails
{
    public class PasswordResetEmail : IEmail
    {
        public string TemplateFilename{ get; private set; }
        public string PwdResetLink { get; set; }

        public PasswordResetEmail()
        {
            TemplateFilename = "passwordreset.html";
        }

        public string FillTemplate(string template)
        {
            return template.Replace("{{PwdResetLink}}", PwdResetLink);
        }
    }
}