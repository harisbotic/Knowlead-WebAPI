using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.Common.Exceptions;
using Knowlead.DAL;
using Knowlead.DomainModel.LookupModels.Core;
using Knowlead.DomainModel.UserModels;
using Microsoft.EntityFrameworkCore;
using static Knowlead.Common.Constants;

namespace Knowlead.BLL.Repositories
{
    public class RewardRepository : IRewardRepository
    {
        private readonly ApplicationDbContext _context;

        public RewardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Reward>> GetAllRewards(string codeStartsWith = null)
        {
            if(codeStartsWith == null)
                return await _context.Rewards.ToListAsync();
            else
                return await _context.Rewards.Where(x => x.Code.StartsWith(codeStartsWith)).ToListAsync();
        }

        public async Task<Reward> GetReward(int rewardId)
        {
            return await _context.Rewards.Where(x => x.CoreLookupId == rewardId).FirstOrDefaultAsync();
        }

        public async Task<Reward> GetReward(string rewardCode)
        {
            return await _context.Rewards.Where(x => x.Code == rewardCode).FirstOrDefaultAsync();
        }

        public async Task<bool> GotReward(Guid applicationUserId, int rewardId)
        {
            return await _context.ApplicationUserRewards.Where(x => x.ApplicationUserId.Equals(applicationUserId) &&
                                                                    x.RewardId == rewardId).FirstOrDefaultAsync() != null;
        }

        public async Task<List<int>> GetClaimedRewards(Guid applicationUserId)
        {
            return await _context.ApplicationUserRewards.Where(x => x.ApplicationUserId.Equals(applicationUserId)).Select(x => x.RewardId).ToListAsync();
        }

        public async Task ClaimReward(Guid applicationUserId, int rewardId)
        {
            _context.ApplicationUserRewards.Add(new ApplicationUserReward(applicationUserId, rewardId));
            await SaveChangesAsync();
        }

        private async Task SaveChangesAsync()
        {
            var success = await _context.SaveChangesAsync() > 0;
            if (!success)
                throw new ErrorModelException(ErrorCodes.DatabaseError); //No changed were made to entity
        }
    }
}