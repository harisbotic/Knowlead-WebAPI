using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.Common.DataAnnotations;
using Knowlead.DTO.P2PModels;
using Knowlead.DTO.UserModels;

namespace Knowlead.DTO.NotificationModels
{
    public class Notification
    {
        [Key]
        public Guid NotificationId { get; set; }
        [MyRequired]
        public String NotificationType { get; private set; }

        [MyRequired]
        public Guid ForApplicationUserId { get; set; }
        public ApplicationUserModel ForApplicationUser { get; set; }
        
        public Guid FromApplicationUserId { get; set; }
        public ApplicationUserModel FromApplicationUser { get; set; }
        
        public int? P2PId { get; set; }
        public P2PModel P2P { get; set; }

        [MyRequired]
        public DateTime ScheduledAt { get; set; }
        public DateTime? SeenAt { get; set; }

        public Notification()
        {   }
    }
}