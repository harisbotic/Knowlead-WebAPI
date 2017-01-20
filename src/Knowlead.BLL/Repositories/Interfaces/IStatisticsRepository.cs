using System;
using System.Threading.Tasks;
using Knowlead.DomainModel.StatisticsModels;

namespace Knowlead.BLL.Repositories.Interfaces
{
    public interface IStatisticsRepository
    {
        Task<PlatformFeedback> SubmitFeedback(String text, Guid ApplicationUserId);
    }
}