using System;
using Knowlead.Common.DataAnnotations;
using Knowlead.DTO.BlobModels;

namespace Knowlead.DTO.P2PModels
{
    public class P2PFileModel : EntityBaseModel
    {
        [MyRequired]
        public int P2pId { get; set; }
        public P2PModel P2p{ get; set; }

        [MyRequired]
        public Guid FileBlobId { get; set; }
        public FileBlobModel FileBlob { get; set; }
      
    }
}