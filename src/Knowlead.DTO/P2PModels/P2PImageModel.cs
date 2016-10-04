using Knowlead.Common.DataAnnotations;
using Knowlead.DTO.CoreModels;

namespace Knowlead.DTO.P2PModels
{
    public class P2PImageModel
    {
        [MyRequired]
        public int P2pId { get; set; }
        public P2PModel P2p{ get; set; }

        [MyRequired]
        public int ImageId { get; set; }
        public ImageModel Image { get; set; }
      
    }
}