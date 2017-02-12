using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.Common.DataAnnotations;
using Knowlead.DTO.P2PModels;
using Knowlead.DTO.UserModels;

namespace Knowlead.DTO.NotificationModels
{
    public class NotificationModel
    {
        [Key]
        public Guid NotificationId { get; set; }
        
        [MyRequired]
        public String NotificationType { get; private set; }
        
        public Guid FromApplicationUserId { get; set; }
        public ApplicationUserModel FromApplicationUser { get; set; }
        
        public int? P2pId { get; set; } //TODO: change to P2P when frontend matures
        public P2PModel P2p { get; set; }

        [MyRequired]
        public DateTime ScheduledAt { get; set; }
        public DateTime? SeenAt { get; set; }

        public NotificationModel()
        {   }
    }
}