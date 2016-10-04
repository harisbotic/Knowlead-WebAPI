using Knowlead.Common.DataAnnotations;
using Knowlead.DTO.CoreModels;

namespace Knowlead.DTO.P2PModels
{
    public class P2PFileModel
    {
        [MyRequired]
        public int P2pId { get; set; }
        public P2PModel P2p{ get; set; }

        [MyRequired]
        public int UploadedFileId { get; set; }
        public UploadedFileModel UploadedFile { get; set; }
      
    }
}