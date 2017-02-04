using System;
using System.Threading.Tasks;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.Common.Exceptions;
using Knowlead.DomainModel.LookupModels.Core;
using Knowlead.DTO.StatisticsModels;
using Knowlead.Services.Interfaces;
using static Knowlead.Common.Constants;

namespace Knowlead.Services
{
    public class RewardServices : IRewardServices
    {
        private readonly IRewardRepository _rewardRepository;
        private readonly ITransactionServices _transactionServices;
        private readonly INotificationServices _notificationServices;
        private readonly IAccountRepository _accountRepository;

        public RewardServices(IRewardRepository rewardRepository, ITransactionServices transactionServices,
                              INotificationServices notificationServices, IAccountRepository accountRepository)
        {
            _rewardRepository = rewardRepository;
            _transactionServices = transactionServices;
            _notificationServices = notificationServices;
            _accountRepository = accountRepository;
        }

        public Task CheckRewards(Guid applicationUserId)
        {
            throw new NotImplementedException();
        }

        public async Task<Reward> ClaimReward(Guid applicationUserId, int rewardId)
        {
            var reward = await _rewardRepository.GetReward(rewardId);

            if(reward == null)
                throw new ErrorModelException(ErrorCodes.EntityNotFound, nameof(Reward));

            if(!await CanClaimReward(applicationUserId, reward))
                throw new ErrorModelException(ErrorCodes.ClaimRewardError);

            //Claim reward
            await _rewardRepository.ClaimReward(applicationUserId, rewardId);

            //Make minutes/points transaction
            await _transactionServices.RewardMinutes(applicationUserId, reward.MinutesReward, reward.PointsReward, TransactionReasons.RewardClaimed+rewardId);

            //Send notification
            await _notificationServices.NewNotification(applicationUserId, NotificationTypes.RewardClaimed, DateTime.UtcNow);
            
            return reward;
        }

        public async Task<bool> CanClaimReward(Guid applicationUserId, Reward reward)
        {
            if(await _rewardRepository.GotReward(applicationUserId, reward.CoreLookupId))
                throw new ErrorModelException(ErrorCodes.AlreadyGotReward);

            if(reward.Code.StartsWith("ref"))
            {
                var refNumber = Convert.ToInt32(reward.Code.Substring(3));
            
                return await _accountRepository.GetReferralsCount(applicationUserId) >= refNumber;
            }

            return false;
        }

        /* TEMP CODE */
        public async Task<ReferralStatsModel> GetReferralStats(Guid applicationUserId)
        {
            var refStats = new ReferralStatsModel();
            var registratedRefs = await _accountRepository.GetReferrals(applicationUserId, true);
            var unregisteredRefs = await _accountRepository.GetReferrals(applicationUserId, false);
            var allRefRewards = await _rewardRepository.GetAllRewards("ref");
            var claimedRewards = await _rewardRepository.GetClaimedRewards(applicationUserId);

            refStats.RegistratedReferralsCount = registratedRefs.Count;
            refStats.UnregisteredReferralsCount = unregisteredRefs.Count;
            
            foreach (var reward in allRefRewards)
            {
                if(claimedRewards.Contains(reward.CoreLookupId))
                    refStats.RewardsClaimed.Add(reward.CoreLookupId);

                else if (await CanClaimReward(applicationUserId, reward))
                    refStats.RewardsAvailable.Add(reward.CoreLookupId);
            }

            foreach (var @ref in unregisteredRefs)
            {
                var status = @ref.NewRegistredUser.EmailConfirmed ? "REGISTRATION" : "EMAIL";
                refStats.UnregisteredReferrals.Add(@ref.NewRegistredUser.Email, status);
            }
            
            return refStats;
        }
        /* TEMP CODE */

    }
}