using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.UserModels;

namespace Knowlead.DomainModel.P2PModels
{
    public class P2PMessage
    {
        [Key]
        public int P2pMessageId { get; set; }
        
        [MyRequired]
        public string Text { get; set; }

        public DateTime Timestamp { get; set; }

        [MyRequired]
        public int P2pId { get; set; }
        public P2P P2p { get; set; }

        [MyRequired]
        public Guid MessageToId { get; set; }
        public ApplicationUser MessageTo { get; set; }
    
        [MyRequired]
        public Guid MessageFromId { get; set; }
        public ApplicationUser MessageFrom { get; set; }

        public P2PMessage ()
        {
            this.Timestamp = DateTime.UtcNow;
        }
    }
}