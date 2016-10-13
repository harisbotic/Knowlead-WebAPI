using System;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.CoreModels;

namespace Knowlead.DomainModel.P2PModels
{
    public class P2PImage
    {
        [MyRequired]
        public int P2pId { get; set; }
        public P2P P2p{ get; set; }

        [MyRequired]
        public Guid ImageId { get; set; }
        public Image Image { get; set; }
      
    }
}