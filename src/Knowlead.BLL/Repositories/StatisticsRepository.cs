using System;
using System.Net;
using System.Threading.Tasks;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.DAL;
using Knowlead.DomainModel.StatisticsModels;
using Knowlead.DomainModel.UserModels;
using Knowlead.Services;

namespace Knowlead.BLL.Repositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly ApplicationDbContext _context;
        private MessageServices _messageServices;
        private IAccountRepository _accountRepository;

        public StatisticsRepository(ApplicationDbContext context, MessageServices messageServices, IAccountRepository accountRepository)
        {
            _context = context;
            _messageServices = messageServices;
            _accountRepository = accountRepository;
        }

        public async Task<PlatformFeedback> SubmitPlatformFeedback(String feedback, Guid applicationUserId)
        {
            var platformFeedback = new PlatformFeedback() { Feedback = feedback, SubmittedById = applicationUserId };
            
            _context.PlatformFeedbacks.Add(platformFeedback);
            await _context.SaveChangesAsync();
            
            ApplicationUser user = await _accountRepository.GetApplicationUserById(applicationUserId);
            var nameSurname = WebUtility.HtmlEncode(user.Name) + " " + WebUtility.HtmlEncode(user.Surname);
            var text = WebUtility.HtmlEncode(feedback);
            await _messageServices.TempSendEmailAsync("feedback@knowlead.co", "New Feedback - " + nameSurname, user.Email, nameSurname, text);

            return platformFeedback;
        }
    }
}