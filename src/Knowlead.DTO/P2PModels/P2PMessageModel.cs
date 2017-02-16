using System;
using Knowlead.Common.DataAnnotations;
using Knowlead.DTO.P2PModels;
using Knowlead.DTO.UserModels;

namespace Knowlead.DomainModel.P2PModels
{
    public class P2PMessageModel
    {
        public int P2pMessageId { get; set; }
        
        [MyRequired]
        public string Text { get; set; }

        [MyRequired]
        public DateTime DateTimeOffer { get; set; }
        
        [MyRequired]
        public int PriceOffer { get; set; }

        public DateTime Timestamp { get; set; }

        [MyRequired]
        public int P2pId { get; set; }
        public P2PModel P2p { get; set; }

        [MyRequired]
        public Guid MessageToId { get; set; }
        public ApplicationUserModel MessageTo { get; set; }
    
        public Guid MessageFromId { get; set; }
        public ApplicationUserModel MessageFrom { get; set; }

        public int? OfferAcceptedId { get; set; }
        public P2PMessage OfferAccepted { get; set; }
    }
}