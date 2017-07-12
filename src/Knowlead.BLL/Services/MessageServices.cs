using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Knowlead.Services.Interfaces;
using Knowlead.Common.Email.Interfaces;
using Newtonsoft.Json;

namespace Knowlead.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class MessageServices : IEmailSender
    {
        private static Dictionary<string, string> _templateCache = new Dictionary<string, string>();

        public async Task<bool> SendEmailAsync(string toAddress, string subject, IEmail emailData, string fromAddress = "knowlead@knowlead.co", string fromName = "Knowlead")
        {
            if(!_templateCache.ContainsKey(emailData.TemplateFilename))
            {
                try
                {
                    string templatePath = $"{Environment.CurrentDirectory}/../Knowlead.Common/Email/Templates/{emailData.TemplateFilename}";
                    string templateContent = System.IO.File.ReadAllText(templatePath);
                    _templateCache.Add(emailData.TemplateFilename, templateContent);
                }
                catch
                {
                    Console.WriteLine("Can't load file");
                    return false;
                }
            }

            string template = _templateCache[emailData.TemplateFilename];
            string emailContent = emailData.FillTemplate(template);

            return await SendEmailAsync(toAddress, subject, emailContent, fromAddress, fromName);
        }

        public async Task<bool> SendEmailAsync(string toAddress, string subject, string content, string fromAddress = "knowlead@knowlead.co", string fromName = "Knowlead")
        {
            HttpClient http = new HttpClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "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");
            http.BaseAddress = new Uri("https://api.sendgrid.com/v3/");

            SendGridMessage message = new SendGridMessage(new SendGridEmail(toAddress), subject, new SendGridEmail(fromAddress, fromName), content);
            try
            {
                string json = JsonConvert.SerializeObject(message);
                HttpResponseMessage response = await http.PostAsync("mail/send", new StringContent(json, Encoding.UTF8, "application/json"));

                if (!response.IsSuccessStatusCode)
                {
                    string errorJson = await response.Content.ReadAsStringAsync();
                    throw new Exception($"SendGrid indicated failure, code: {response.StatusCode}, reason: {errorJson}");
                }
            }
            catch
            {
                Console.WriteLine("Can't send mail");
                return false;
            }

            return true;
        }

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
    }
}