using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.DomainModel.LookupModels.Core;

namespace Knowlead.DomainModel.UserModels
{
  public class UserAchievement
  {
    [Key]
    public int UserAchievementId { get; set; }
    
    public DateTime CreatedAt { get; set; }

    public Guid ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }

    public int AchievementId { get; set; }
    //[ForeignKey("AchievementId")]
    public Achievement Achievement { get; set; } 

    public UserAchievement()
    {
        CreatedAt = DateTime.UtcNow;
    }
  }
}