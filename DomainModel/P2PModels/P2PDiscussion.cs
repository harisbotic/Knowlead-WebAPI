using System.ComponentModel.DataAnnotations;

namespace Knowlead.DomainModel.P2PModels
{
    public class P2PDiscussion
    {
        [Key]
        public int P2pDiscussionId { get; set; }
        public string Text { get; set; }

        public int ResponseToId { get; set; }
        public P2PDiscussion ResponseTo { get; set; } // not required
        
        public int P2pId { get; set; }
        public P2P P2p { get; set; }
    }
}