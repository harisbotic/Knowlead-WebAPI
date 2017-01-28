using System;
using System.Collections.Generic;

namespace Knowlead.DTO.StatisticsModels
{
    public class ReferralStatsModel
    {
        public int RegistratedReferralsCount { get; set; }
        public int UnregisteredReferralsCount { get; set; }

        public List<int> RewardsClaimed { get; set; } = new List<int>();
        public List<int> RewardsAvailable { get; set; } = new List<int>();

        public Dictionary<String, String> UnregisteredReferrals { get; set; } = new Dictionary<String, String>();

    }
}