using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.P2PModels;
using Knowlead.DomainModel.UserModels;

namespace Knowlead.DomainModel.NotificationModels
{
    public class Notification
    {
        [Key]
        public Guid NotificationId { get; set; }
        [MyRequired]
        public String NotificationType { get; private set; }

        [MyRequired]
        public Guid ForApplicationUserId { get; set; }
        public ApplicationUser ForApplicationUser { get; set; }
        
        public Guid FromApplicationUserId { get; set; }
        public ApplicationUser FromApplicationUser { get; set; }
        
        public int? P2PMessageId { get; set; }
        public P2PMessage P2PMessage { get; set; }

        public int? P2PId { get; set; }
        public P2P P2P { get; set; }

        [MyRequired]
        public DateTime ScheduledAt { get; set; }
        public DateTime? SeenAt { get; set; }

        public Notification(Guid forApplicationUser, String notificationType, DateTime scheduledAt, P2P p2p, P2PMessage p2pMessage)
                            :this(forApplicationUser, notificationType, scheduledAt)
        {   
            this.P2P = p2p;
            this.P2PMessage = p2pMessage;
        }

        public Notification(Guid forApplicationUser, String notificationType, DateTime scheduledAt)
        {   
            this.ForApplicationUserId = forApplicationUser;
            this.NotificationType = notificationType;
            this.ScheduledAt = scheduledAt;
        }

        public Notification()
        {   }
    }
}