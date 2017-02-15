using System;
using System.Threading.Tasks;
using Knowlead.DomainModel.LookupModels.FeedbackModels;
using Knowlead.DTO.LookupModels.FeedbackModels;

namespace Knowlead.Services.Interfaces
{
    public interface IFeedbackServices
    {
        Task<P2PFeedback> GiveP2PFeedback(P2PFeedbackModel p2pFeedbackModel, Guid applicationUserId);
    }
}
