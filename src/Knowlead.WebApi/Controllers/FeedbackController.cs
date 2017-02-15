using System.Threading.Tasks;
using Knowlead.Common.Attributes;
using Knowlead.Common.HttpRequestItems;
using Knowlead.DTO.LookupModels.FeedbackModels;
using Knowlead.DTO.ResponseModels;
using Knowlead.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Knowlead.Common.Constants;

namespace Knowlead.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = Policies.RegisteredUser)]
    public class FeedbackController : Controller
    {
        private readonly IFeedbackServices _feedbackServices;
        private readonly Auth _auth;

        public FeedbackController(IFeedbackServices feedbackServices, Auth auth)
        {
            _feedbackServices = feedbackServices;
            _auth = auth;
        }

        [HttpPost("give/p2p"), ValidateModel] 
        public async Task<IActionResult> GiveFeedback([FromBody]P2PFeedbackModel p2pFeedbackModel)
        {
            var applicationUserId = _auth.GetUserId();

            var p2pFeedback = await _feedbackServices.GiveP2PFeedback(p2pFeedbackModel, applicationUserId);
            
            return Ok(new ResponseModel{
                Object = p2pFeedback
            });
        }
    }
}