using System;
using System.Threading.Tasks;
using Knowlead.DomainModel.StatisticsModels;

namespace Knowlead.BLL.Repositories.Interfaces
{
    public interface IStatisticsRepository
    {
        Task<PlatformFeedback> SubmitPlatformFeedback(String feedback, Guid ApplicationUserId);
    }
}