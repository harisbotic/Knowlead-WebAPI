using System;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.P2PModels;
using Knowlead.DomainModel.UserModels;

namespace Knowlead.DomainModel.CallModels
{
    public class P2PCall : _Call
    {
        [MyRequired]
        public int P2PId { get; set; }
        public P2P P2P { get; set; }
        [MyRequired]
        public Guid CallReceiverId { get; set; }
        public ApplicationUser CallReceiver { get; set; }

        public P2PCall() : base()
        {
        }
    }
}