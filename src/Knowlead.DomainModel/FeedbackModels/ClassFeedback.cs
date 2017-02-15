using System;
using System.Collections.Generic;
using Knowlead.DomainModel.FeedbackModels;

namespace Knowlead.DomainModel.LookupModels.FeedbackModels
{
    public class ClassFeedback : _Feedback
    {
        public override void CalculateRating()
        {
            throw new NotImplementedException();
        }

        public override Dictionary<string, int> GetRatingParameters()
        {
            throw new NotImplementedException();
        }
    }
}