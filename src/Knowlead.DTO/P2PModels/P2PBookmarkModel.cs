using System;
using Knowlead.Common.DataAnnotations;
using Knowlead.DTO.UserModels;

namespace Knowlead.DTO.P2PModels
{
    public class P2PBookmarkModel
    {
        [MyRequired]
        public int P2pId { get; set; }
        public P2PModel P2p{ get; set; }

        [MyRequired]
        public Guid ApplicationUserId { get; set; }
        public ApplicationUserModel ApplicationUser { get; set; }
      
    }
}