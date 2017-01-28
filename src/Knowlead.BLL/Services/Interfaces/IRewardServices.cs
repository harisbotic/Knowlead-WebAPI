using System;
using System.Threading.Tasks;
using Knowlead.DomainModel.LookupModels.Core;
using Knowlead.DTO.StatisticsModels;

namespace Knowlead.Services.Interfaces
{
    public interface IRewardServices
    {
        Task<Reward> ClaimReward (Guid applicationUserId, int rewardId);
        Task CheckRewards (Guid applicationUserId);
        Task<ReferralStatsModel> GetReferralStats(Guid applicationUserId);
    }
}
