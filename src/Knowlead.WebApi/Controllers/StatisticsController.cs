using System.Threading.Tasks;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.Common.HttpRequestItems;
using Knowlead.DTO.ResponseModels;
using Knowlead.DTO.StatisticsModels;
using Knowlead.WebApi.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Knowlead.Controllers
{
    [Route("api/[controller]")]
    public class StatisticsController : Controller
    {
        IStatisticsRepository _statisticsRepository;
        Auth _auth;
        public StatisticsController(IStatisticsRepository statisticsRepository, Auth auth)
        {
            this._statisticsRepository = statisticsRepository;
            this._auth = auth;
        }
        
        [HttpPost("feedback"), ReallyAuthorize]
        public async Task<IActionResult> Feedback([FromBody]PlatformFeedbackModel feedback)
        {
            return new OkObjectResult(new ResponseModel()
            {
                Object = await this._statisticsRepository.SubmitFeedback(feedback.Text, this._auth.GetUserId())
            });
        }
    }
}