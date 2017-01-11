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
        
        [MyRequired]
        public Guid FromApplicationUserId { get; set; }
        public ApplicationUser FromApplicationUser { get; set; }
        
        [MyRequired]
        public int P2PId { get; set; }
        public P2P P2P { get; set; }

        public DateTime? SentAt { get; set; }
        public DateTime? SeenAt { get; set; }

        public Notification(Guid forApplicationUser, String notificationType)
        {
            this.SentAt = DateTime.UtcNow;
            
            this.ForApplicationUserId = forApplicationUser;
            this.NotificationType = notificationType;
        }
    }
}