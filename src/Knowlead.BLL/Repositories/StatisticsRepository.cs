using System;
using System.Threading.Tasks;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.DAL;
using Knowlead.DomainModel.StatisticsModels;

namespace Knowlead.BLL.Repositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly ApplicationDbContext _context;
        
        public StatisticsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PlatformFeedback> SubmitPlatformFeedback(String feedback, Guid ApplicationUserId)
        {
            var platformFeedback = new PlatformFeedback() { Feedback = feedback, SubmittedById = ApplicationUserId };
            
            _context.PlatformFeedbacks.Add(platformFeedback);
            await _context.SaveChangesAsync();
            
            return platformFeedback;
        }
    }
}