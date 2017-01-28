using System.Threading.Tasks;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.Common.HttpRequestItems;
using Knowlead.DTO.LookupModels.Core;
using Knowlead.DTO.ResponseModels;
using Knowlead.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Knowlead.Common.Constants;

namespace Knowlead.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = Policies.RegisteredUser)]
    public class RewardController : Controller
    {
        private readonly IRewardRepository _rewardRepository;
        private readonly IRewardServices _rewardServices;
        private readonly Auth _auth;

        public RewardController(IRewardRepository rewardRepository, IRewardServices rewardServices, Auth auth)
        {
            _rewardRepository = rewardRepository;
            _rewardServices = rewardServices;
            _auth = auth;
        }
        
        [HttpPost("claim")]
        public async Task<IActionResult> ClaimReward([FromBody]RewardModel rewardModel)
        {
            var reward = await _rewardServices.ClaimReward(_auth.GetUserId(), rewardModel.CoreLookupId);

            if(reward == null)
                return BadRequest();

            return Ok(new ResponseModel()
            {
                Object = AutoMapper.Mapper.Map<RewardModel>(reward)
            });
        }
    }
}