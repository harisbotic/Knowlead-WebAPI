using System;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.LookupModels.Core;

namespace Knowlead.DomainModel.UserModels
{
    public class ApplicationUserReward : EntityBase
    {
        [MyRequired]
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [MyRequired]
        public int RewardId { get; set; }
        public Reward Reward { get; set; }

        public ApplicationUserReward(Guid applicationUserId, int rewardId)
        {
            this.ApplicationUserId = applicationUserId;
            this.RewardId = rewardId;
        }

        public ApplicationUserReward() //For EF
        {
        }
    }
}