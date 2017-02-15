using System.Collections.Generic;
using Knowlead.Common.DataAnnotations;
using Knowlead.DomainModel.FeedbackModels;
using Knowlead.DomainModel.P2PModels;

namespace Knowlead.DomainModel.LookupModels.FeedbackModels
{
    public class P2PFeedback : _Feedback
    {
        [MyRequired]
        public int Expertise { get; set; }

        [MyRequired]
        public int Helpful { get; set; }

        [MyRequired]
        public int P2pId { get; set; }
        public P2P P2p { get; set; }

        public override void CalculateRating() => this.Rating = (this.Expertise + this.Helpful) / 2;

        public override Dictionary<string, int> GetRatingParameters()
        {
            return new Dictionary<string, int>{{nameof(Expertise), Expertise}, {nameof(Helpful), Helpful}};
        }
    }
}