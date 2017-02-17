using System;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.UserModels;

namespace Knowlead.DomainModel.P2PModels
{
    public class P2PBookmark
    {
        [MyRequired]
        public int P2pId { get; set; }
        public P2P P2p{ get; set; }

        [MyRequired]
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
      
    }
}