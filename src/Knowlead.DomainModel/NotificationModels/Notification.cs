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
        
        public Guid? FromApplicationUserId { get; set; }
        public ApplicationUser FromApplicationUser { get; set; }
        
        public int? P2pMessageId { get; set; }
        public P2PMessage P2pMessage { get; set; }

        public int? P2pId { get; set; }
        public P2P P2p { get; set; }

        [MyRequired]
        public DateTime ScheduledAt { get; set; }
        public DateTime? SeenAt { get; set; }

        public Notification(Guid forApplicationUser, String notificationType, DateTime scheduledAt, Guid fromApplicationUserId, P2P p2p, P2PMessage p2pMessage)
                            :this(forApplicationUser, notificationType, scheduledAt)
        {   
            this.FromApplicationUserId = fromApplicationUserId;

            this.P2p = p2p;
            this.P2pId = p2p?.P2pId;

            this.P2pMessage = p2pMessage;
            this.P2pMessageId = p2pMessage?.P2pMessageId;

            if(this.P2pId == null)
                this.P2pId = p2pMessage?.P2pId;
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