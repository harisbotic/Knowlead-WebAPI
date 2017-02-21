using System;
using System.Threading.Tasks;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.DAL;
using Knowlead.DomainModel.StatisticsModels;
using Knowlead.Services;

namespace Knowlead.BLL.Repositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly ApplicationDbContext _context;
        private MessageServices _messageServices;

        public StatisticsRepository(ApplicationDbContext context, MessageServices messageServices)
        {
            _context = context;
            _messageServices = messageServices;
        }

        public async Task<PlatformFeedback> SubmitPlatformFeedback(String feedback, Guid applicationUserId)
        {
            var platformFeedback = new PlatformFeedback() { Feedback = feedback, SubmittedById = applicationUserId };
            
            _context.PlatformFeedbacks.Add(platformFeedback);
            await _context.SaveChangesAsync();
            
            await _messageServices.TempSendEmailAsync("feedback@knowlead.co","New Platform Feedback", "haris.botic96@@gmail.com", applicationUserId.ToString(), feedback);

            return platformFeedback;
        }
    }
}