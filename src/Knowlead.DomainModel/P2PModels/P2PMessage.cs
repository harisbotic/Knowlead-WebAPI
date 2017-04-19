using System;
using System.ComponentModel.DataAnnotations;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.UserModels;

namespace Knowlead.DomainModel.P2PModels
{
    public class P2PMessage : EntityBase
    {
        [Key]
        public int P2pMessageId { get; set; }
        
        [MyRequired]
        public string Text { get; set; }

        [MyRequired]
        public DateTime DateTimeOffer { get; set; }

        [MyRequired]
        public int PriceOffer { get; set; }

        [MyRequired]
        public DateTime Timestamp { get; set; } //TODO: Use inherited

        [MyRequired]
        public int P2pId { get; set; }
        public P2P P2p { get; set; }

        [MyRequired]
        public Guid MessageToId { get; set; }
        public ApplicationUser MessageTo { get; set; }
    
        [MyRequired]
        public Guid MessageFromId { get; set; }
        public ApplicationUser MessageFrom { get; set; }

        public int? OfferAcceptedId { get; set; }
        public P2PMessage OfferAccepted { get; set; }

        public P2PMessage ()
        {
            this.Timestamp = DateTime.UtcNow;
        }
    }
}