using System.Threading.Tasks;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.Common.HttpRequestItems;
using Knowlead.DTO.ResponseModels;
using Knowlead.DTO.StatisticsModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Knowlead.Common.Constants;

namespace Knowlead.Controllers
{
    [Route("api/[controller]")]
    public class StatisticsController : Controller
    {
        private readonly IStatisticsRepository _statisticsRepository;
        private readonly Auth _auth;

        public StatisticsController(IStatisticsRepository statisticsRepository, Auth auth)
        {
            _statisticsRepository = statisticsRepository;
            _auth = auth;
        }
        
        [HttpPost("feedback"), Authorize(Policy = Policies.RegisteredUser)]
        public async Task<IActionResult> Feedback([FromBody]PlatformFeedbackModel feedbackModel)
        {
            var platformFeedback = await _statisticsRepository.SubmitPlatformFeedback(feedbackModel.Feedback, _auth.GetUserId());

            return Ok(new ResponseModel()
            {
                Object = AutoMapper.Mapper.Map<PlatformFeedbackModel>(platformFeedback)
            });
        }
    }
}