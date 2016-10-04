using Knowlead.Common.DataAnnotations;

namespace Knowlead.DTO.P2PModels
{
    public class P2PDiscussionModel
    {
        [MyRequired]
        public string Text { get; set; }

        public int? ResponseToId { get; set; }
        public P2PDiscussionModel ResponseTo { get; set; }
        
        [MyRequired]
        public int P2pId { get; set; }
        public P2PModel P2p { get; set; }
    }
}

//TODO: add time?