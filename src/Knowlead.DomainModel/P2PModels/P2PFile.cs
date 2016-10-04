using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.CoreModels;

namespace Knowlead.DomainModel.P2PModels
{
    public class P2PFile
    {
        [MyRequired]
        public int P2pId { get; set; }
        public P2P P2p{ get; set; }

        [MyRequired]
        public int UploadedFileId { get; set; }
        public UploadedFile UploadedFile { get; set; }
      
    }
}