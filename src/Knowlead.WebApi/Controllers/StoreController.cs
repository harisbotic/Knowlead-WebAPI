using System.Threading.Tasks;
using Knowlead.Common.HttpRequestItems;
using Knowlead.DTO.ResponseModels;
using Knowlead.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Knowlead.Common.Constants;

namespace Knowlead.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = Policies.RegisteredUser)]
    public class StoreController : Controller
    {
        private readonly IRewardServices _rewardServices;
        private readonly Auth _auth;

        public StoreController(IRewardServices rewardServices, Auth auth)
        {
            _rewardServices = rewardServices;
            _auth = auth;
        }
        
        [HttpGet("referralStats")]
        public async Task<IActionResult> ClaimReward()
        {
            var refStats = await _rewardServices.GetReferralStats(_auth.GetUserId());

            return Ok(new ResponseModel()
            {
                Object = refStats
            });
        }
    }
}