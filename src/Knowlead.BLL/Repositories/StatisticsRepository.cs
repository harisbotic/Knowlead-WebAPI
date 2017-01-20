using System;
using System.Threading.Tasks;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.DAL;
using Knowlead.DomainModel.StatisticsModels;

namespace Knowlead.BLL.Repositories
{
    public class StatisticsRepository : IStatisticsRepository
    {
        ApplicationDbContext _context;
        public StatisticsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        async Task<PlatformFeedback> IStatisticsRepository.SubmitFeedback(string text, Guid ApplicationUserId)
        {
            var temp = new PlatformFeedback() { Text = text, SubmittedById = ApplicationUserId };
            _context.PlatformFeedbacks.Add(temp);
            await _context.SaveChangesAsync();
            return temp;
        }
    }
}