using System.Threading.Tasks;
using Knowlead.Common.Attributes;
using Microsoft.AspNetCore.Mvc;
using Knowlead.Common.HttpRequestItems;
using Knowlead.DTO.P2PModels;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.DomainModel.P2PModels;
using Microsoft.AspNetCore.Authorization;
using static Knowlead.Common.Constants;
using static Knowlead.Common.Constants.EnumActions;
using Knowlead.Common.Exceptions;

namespace Knowlead.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = Policies.RegisteredUser)]
    public class P2PController : Controller
    {
        private readonly IP2PRepository _p2pRepository;
        private readonly Auth _auth;

        public P2PController(IP2PRepository p2pRespository,
                             Auth auth)
        {
            _p2pRepository = p2pRespository;
            _auth = auth;
        }

        [HttpGet("{p2pId}")]
        public async Task<IActionResult> GetP2P(int p2pId)
        {
            return await _p2pRepository.GetP2P(p2pId);
        }

        [HttpGet("messages/{p2pId}")]
        public async Task<IActionResult> GetMessages(int p2pId)
        {
            var applicationUserId = _auth.GetUserId();

            return await _p2pRepository.GetMessages(p2pId, applicationUserId);
        }

        [HttpPost("create"), ValidateModel]
        public async Task<IActionResult> Create([FromBody] P2PModel p2pModel)
        {
            var applicationUserId = _auth.GetUserId();

            return await _p2pRepository.Create(p2pModel, applicationUserId);
        }

        [HttpPost("schedule")]
        public async Task<IActionResult> Schedule(int p2pMessageId)
        {
            var applicationUserId = _auth.GetUserId();

            return await _p2pRepository.Schedule(p2pMessageId, applicationUserId);
        }

        [HttpPost("message"), ValidateModel]
        public async Task<IActionResult> Message([FromBody] P2PMessageModel p2pMessageModel)
        {
            var applicationUserId = _auth.GetUserId();

            return await _p2pRepository.Message(p2pMessageModel, applicationUserId);
        }

        [HttpDelete("delete/{p2pId}")]
        public async Task<IActionResult> Delete(int p2pId)
        {
            var applicationUserId = _auth.GetUserId();

            return await _p2pRepository.Delete(p2pId, applicationUserId);
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            return await _p2pRepository.ListAll();
        }

        [HttpGet("list/{listP2PRequest}")]
        public async Task<IActionResult> ChangeFriendshipStatus(ListP2PsRequests listP2PRequest)
        {
            var applicationUserId = _auth.GetUserId();
            
            // List<P2P> result = null;
            // result = await _p2pRepository.ListMine(applicationUserId);
            switch (listP2PRequest) 
            {
                case(ListP2PsRequests.My):
                    return await _p2pRepository.ListMine(applicationUserId);
                    // break;
                
                case(ListP2PsRequests.Scheduled):
                    return await _p2pRepository.ListSchedulded(applicationUserId);
                    // break;
                
                case(ListP2PsRequests.Bookmarked):
                    return await _p2pRepository.ListBookmarked(applicationUserId);
                    // break;

                case(ListP2PsRequests.ActionRequired):
                    return await _p2pRepository.ListActionRequired(applicationUserId);
                    // break;

                case(ListP2PsRequests.Deleted):
                    return await _p2pRepository.ListDeleted(applicationUserId);
                    // break;
                
                default:
                    throw new ErrorModelException(ErrorCodes.IncorrectValue, nameof(FriendshipDTOActions));
            }
            
            // return new OkObjectResult(new ResponseModel{
            //     Object = Mapper.Map<FriendshipModel>(result)
            // });
        }
    }
}