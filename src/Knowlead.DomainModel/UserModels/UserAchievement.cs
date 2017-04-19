using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.LookupModels.Core;

namespace Knowlead.DomainModel.UserModels
{
    public class UserAchievement : EntityBase
    {
        [Key]
        public int UserAchievementId { get; set; }

        [MyRequired]
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [MyRequired]
        public int AchievementId { get; set; }

        public Achievement Achievement { get; set; }
    }
}