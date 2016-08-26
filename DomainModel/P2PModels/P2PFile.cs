using System.ComponentModel.DataAnnotations;
using Knowlead.DomainModel.CoreModels;
using Knowlead.DomainModel.LookupModels.Core;

namespace Knowlead.DomainModel.P2PModels
{
    public class P2PFile
    {
        [Required]
        public int P2pId { get; set; }
        public P2P P2p{ get; set; }

        [Required]
        public int UploadedFileId { get; set; }
        public UploadedFile UploadedFile { get; set; }
      
    }
}