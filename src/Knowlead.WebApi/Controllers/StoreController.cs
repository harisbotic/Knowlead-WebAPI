using System.Threading.Tasks;
using AutoMapper;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.Common.HttpRequestItems;
using Knowlead.DTO;
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
        private readonly IPromoCodeRepository _promoCodeRepository;
        private readonly Auth _auth;

        public StoreController(IRewardServices rewardServices, IPromoCodeRepository promoCodeRepository, Auth auth)
        {
            _rewardServices = rewardServices;
            _promoCodeRepository = promoCodeRepository;
            _auth = auth;
        }
        
        [HttpPost("applyPromoCode")]
        public async Task<IActionResult> ApplyPromoCode([FromQuery] string promocode) //TODO: should be just code
        {
            var promoCode = await _promoCodeRepository.ApplyPromoCode(promocode, _auth.GetUserId());

            return Ok(new ResponseModel {
                Object = Mapper.Map<PromoCodeModel>(promoCode)
            });
        }

        [HttpGet("referralStats")]
        public async Task<IActionResult> ReferralStats()
        {
            var refStats = await _rewardServices.GetReferralStats(_auth.GetUserId());

            return Ok(new ResponseModel()
            {
                Object = refStats
            });
        }
    }
}