using System.Threading.Tasks;

namespace Knowlead.Services
{
    public interface IEmailSender
    {
        // Task SendEmailAsync(string email, string subject, string message);
        Task TempSendEmailAsync(string to, string subject, string fromAddress, string fromName, string message);
    }
}
