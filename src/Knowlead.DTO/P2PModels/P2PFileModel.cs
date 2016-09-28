using System.ComponentModel.DataAnnotations;
using Knowlead.DTO.CoreModels;

namespace Knowlead.DTO.P2PModels
{
    public class P2PFileModel
    {
        [Required]
        public int P2pId { get; set; }
        public P2PModel P2p{ get; set; }

        [Required]
        public int UploadedFileId { get; set; }
        public UploadedFileModel UploadedFile { get; set; }
      
    }
}