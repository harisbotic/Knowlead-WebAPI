using System.Threading.Tasks;
using Knowlead.Common.Email.Interfaces;

namespace Knowlead.Services.Interfaces
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(string toAddress, string subject, IEmail emailData, string fromAddress, string fromName);
        Task<bool> SendEmailAsync(string toAddress, string subject, string content, string fromAddress, string fromName);
    }
}
