using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Knowlead.DomainModel.LookupModels.Core;

namespace Knowlead.BLL.Repositories.Interfaces
{
    public interface IRewardRepository
    {
        Task<List<Reward>> GetAllRewards(string codeStartsWith = null);
        Task<Reward> GetReward(int rewardId);
        Task<bool> GotReward(Guid applicationUserId, int rewardId);
        Task<List<int>> GetClaimedRewards(Guid applicationUserId);
        Task ClaimReward(Guid applicationUserId, int rewardId);
    }
}