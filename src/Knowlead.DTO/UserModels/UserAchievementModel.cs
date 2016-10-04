using System;
using Knowlead.Common.DataAnnotations;
using Knowlead.DTO.LookupModels.Core;

namespace Knowlead.DTO.UserModels
{
    public class UserAchievementModel
    {
        [MyRequired]
        public int UserAchievementId { get; set; }

        [MyRequired]
        public DateTime CreatedAt { get; set; }

        [MyRequired]
        public Guid ApplicationUserId { get; set; }
        public ApplicationUserModel ApplicationUser { get; set; }

        [MyRequired]
        public int AchievementId { get; set; }

        public AchievementModel Achievement { get; set; }

        public UserAchievementModel()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}