using Knowlead.Common.DataAnnotations;
using Knowlead.DTO.FeedbackModels;
using Knowlead.DTO.P2PModels;

namespace Knowlead.DTO.LookupModels.FeedbackModels
{
    public class P2PFeedbackModel : _FeedbackModel
    {
        [MyRequired]
        public int? Expertise { get; set; }

        [MyRequired]
        public int? Helpful { get; set; }

        [MyRequired]
        public int? P2pId { get; set; }
        public P2PModel P2p { get; set; }
    }
}