using System;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.BlobModels;

namespace Knowlead.DomainModel.P2PModels
{
    public class P2PImage : EntityBase
    {
        [MyRequired]
        public int P2pId { get; set; }
        public P2P P2p{ get; set; }

        [MyRequired]
        public Guid ImageBlobId { get; set; }
        public ImageBlob ImageBlob { get; set; }
      
    }
}