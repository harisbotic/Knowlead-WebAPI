using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.P2PModels;
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

        [MyRequired]
        public Guid ForApplicationUserId { get; set; }
        public ApplicationUserModel ForApplicationUser { get; set; }
        
        public Guid? FromApplicationUserId { get; set; }
        public ApplicationUserModel FromApplicationUser { get; set; }
        
        public int? P2pMessageId { get; set; }
        public P2PMessageModel P2pMessage { get; set; }

        public int? P2pId { get; set; }
        public P2PModel P2p { get; set; }

        [MyRequired]
        public DateTime ScheduledAt { get; set; }
        public DateTime? SeenAt { get; set; }

        public NotificationModel(Guid forApplicationUser, String notificationType, DateTime scheduledAt, Guid fromApplicationUserId, P2PModel p2p, P2PMessageModel p2pMessage)
                            :this(forApplicationUser, notificationType, scheduledAt)
        {   
            this.FromApplicationUserId = fromApplicationUserId;
            
            this.P2p = p2p;
            this.P2pId = p2p.P2pId;

            this.P2pMessage = p2pMessage;
            this.P2pMessageId = p2pMessage.P2pMessageId;
        }

        public NotificationModel(Guid forApplicationUser, String notificationType, DateTime scheduledAt)
        {   
            this.ForApplicationUserId = forApplicationUser;
            this.NotificationType = notificationType;
            this.ScheduledAt = scheduledAt;
        }

        public NotificationModel()
        {   }
    }
}