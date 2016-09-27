using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.DomainModel.LookupModels.Core;

namespace Knowlead.DomainModel.UserModels
{
    public class UserAchievement
    {
        [Key]
        public int UserAchievementId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public int AchievementId { get; set; }

        public Achievement Achievement { get; set; }

        public UserAchievement()
        {
            CreatedAt = DateTime.UtcNow;
        }
    }
}