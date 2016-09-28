using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.DTO.LookupModels.Core;

namespace Knowlead.DTO.UserModels
{
    public class UserAchievementModel
    {
        [Required]
        public int UserAchievementId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public Guid ApplicationUserId { get; set; }
        public ApplicationUserModel ApplicationUser { get; set; }

        [Required]
        public int AchievementId { get; set; }

        public AchievementModel Achievement { get; set; }

        public UserAchievementModel()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}