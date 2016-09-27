using System.ComponentModel.DataAnnotations;
using Knowlead.DomainModel.CoreModels;

namespace Knowlead.DomainModel.P2PModels
{
    public class P2PImage
    {
        [Required]
        public int P2pId { get; set; }
        public P2P P2p{ get; set; }

        [Required]
        public int ImageId { get; set; }
        public Image Image { get; set; }
      
    }
}