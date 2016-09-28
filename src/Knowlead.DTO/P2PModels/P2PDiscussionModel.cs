using System.ComponentModel.DataAnnotations;

namespace Knowlead.DTO.P2PModels
{
    public class P2PDiscussionModel
    {
        [Required]
        public string Text { get; set; }

        public int? ResponseToId { get; set; }
        public P2PDiscussionModel ResponseTo { get; set; }
        
        [Required]
        public int P2pId { get; set; }
        public P2PModel P2p { get; set; }
    }
}

//TODO: add time?