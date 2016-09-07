using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Knowlead.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class MessageServices : IEmailSender
    {
        public async Task TempSendEmailAsync(string to, string subject, string fromAddress, string fromName, string message)
        {

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "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");
            client.BaseAddress = new Uri("https://api.sendgrid.com/v3/");

            var msg = new SendGridMessage(new SendGridEmail(to), subject, new SendGridEmail(fromAddress, fromName), message);
            try
            {
                string json = JsonConvert.SerializeObject(msg);
                var response = await client.PostAsync("mail/send", new StringContent(json, Encoding.UTF8, "application/json"));

                if (!response.IsSuccessStatusCode)
                {
                    string errorJson = await response.Content.ReadAsStringAsync();
                    throw new Exception($"SendGrid indicated failure, code: {response.StatusCode}, reason: {errorJson}");
                }

            }
            catch (Exception)
            {
                Console.WriteLine("jel to");
            }
        }
        /*public Task SendEmailAsync(string email, string subject, string url)
        {
            var myMessage = new SendGrid.SendGridMessage();
            myMessage.AddTo(email);
            myMessage.From = new System.Net.Mail.MailAddress("haris.botic96@gmail.com", "KnowLead");
            myMessage.Subject = subject;
            myMessage.Text = url;
            myMessage.Html = url;
            var credentials = new System.Net.NetworkCredential(
                "Knowlead",
                "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");
            // Create a Web transport for sending email.
            var transportWeb = new SendGrid.Web(credentials);
            return transportWeb.DeliverAsync(myMessage);
        }*/
    }
}