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
using Knowlead.DTO.ResponseModels;
using AutoMapper;

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
            var applicationUserId = _auth.GetUserId();
            return await _p2pRepository.GetP2P(p2pId, applicationUserId);
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

        [HttpPost("schedule/{p2pMessageId}")]
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

        [HttpPost("acceptOffer/{p2pMessageId}")]
        public async Task<IActionResult> AcceptOffer([FromRoute] int p2pMessageId)
        {
            var applicationUserId = _auth.GetUserId();

            return await _p2pRepository.AcceptOffer(p2pMessageId, applicationUserId);
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
            var applicationUserId = _auth.GetUserId();
            return await _p2pRepository.ListAll(applicationUserId);
        }

        [HttpGet("list/{listP2PRequest}")]
        public async Task<IActionResult> ChangeFriendshipStatus(ListP2PsRequest listP2PRequest)
        {
            var applicationUserId = _auth.GetUserId();
            
            // List<P2P> result = null;
            // result = await _p2pRepository.ListMine(applicationUserId);
            switch (listP2PRequest) 
            {
                case(ListP2PsRequest.My):
                    return await _p2pRepository.ListMine(applicationUserId);
                    // break;
                
                case(ListP2PsRequest.Scheduled):
                    return await _p2pRepository.ListSchedulded(applicationUserId);
                    // break;
                
                case(ListP2PsRequest.Bookmarked):
                    return await _p2pRepository.ListBookmarked(applicationUserId);
                    // break;

                case(ListP2PsRequest.ActionRequired):
                    return await _p2pRepository.ListActionRequired(applicationUserId);
                    // break;

                case(ListP2PsRequest.Deleted):
                    return await _p2pRepository.ListDeleted(applicationUserId);
                    // break;
                
                default:
                    throw new ErrorModelException(ErrorCodes.IncorrectValue, nameof(ListP2PsRequest));
            }
        }

        [HttpPost("bookmarkAdd/{p2pId}")]
        public async Task<IActionResult> AddBookmark(int p2pId)
        {
            var applicationUserId = _auth.GetUserId();
            var p2p = await _p2pRepository.AddBookmark(p2pId, applicationUserId);
            
            return Ok(new ResponseModel{
                Object = Mapper.Map<P2PModel>(p2p)
            });
        }

        [HttpPost("bookmarkRemove/{p2pId}")]
        public async Task<IActionResult> RemoveBookmark(int p2pId)
        {
            var applicationUserId = _auth.GetUserId();
            var successful = await _p2pRepository.RemoveBookmark(p2pId, applicationUserId);
            
            if(successful)
                return Ok(new ResponseModel());
            return BadRequest(new ResponseModel());
        }
    }
}