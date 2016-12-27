using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Knowlead.Common.HttpRequestItems;
using Knowlead.WebApi.Attributes;
using Knowlead.BLL.Repositories.Interfaces;
using static Knowlead.Common.Constants;
using Knowlead.DTO.ResponseModels;
using Knowlead.DomainModel.ChatModels;
using Knowlead.DTO.ChatModels;
using static Knowlead.Common.Constants.EnumActions;
using AutoMapper;
using System.Collections.Generic;
using Knowlead.Common.Exceptions;

namespace Knowlead.Controllers
{
    [Route("api/[controller]")]
    public class ChatController : Controller
    {
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly Auth _auth;

        public ChatController(IFriendshipRepository friendshipRepository,
                                          Auth auth)
        {
            _friendshipRepository = friendshipRepository;
            _auth = auth;
        }

        [HttpPost("changefriendshipstatus"), ReallyAuthorize]
        public async Task<IActionResult> ChangeFriendshipStatus([FromBody] ChangeFriendshipStatusModel cfsm)
        {
            var currentUserId = _auth.GetUserId();
            var otherUserId = cfsm.ApplicationUserId;
            
            var action = cfsm.Action;
            Friendship result = null;
            switch (action) 
            {
                case(FriendshipDTOActions.NewRequest):
                    result = await _friendshipRepository.SendFriendRequest(currentUserId, otherUserId);
                    break;
                
                case(FriendshipDTOActions.AcceptRequest):
                    result = await _friendshipRepository.RespondToFriendRequest(currentUserId, otherUserId, true);
                    break;
                
                case(FriendshipDTOActions.DeclineRequest):
                    result = await _friendshipRepository.RespondToFriendRequest(currentUserId, otherUserId, false);
                    break;

                case(FriendshipDTOActions.CancelRequest):
                    result = await _friendshipRepository.CancelFriendRequest(currentUserId, otherUserId);
                    break;

                case(FriendshipDTOActions.RemoveFriend):
                    result = await _friendshipRepository.RemoveFriend(currentUserId, otherUserId);
                    break;

                case(FriendshipDTOActions.BlockUser):
                    result = await _friendshipRepository.BlockUser(currentUserId, otherUserId);
                    break;

                case(FriendshipDTOActions.UnblockUser):
                    result = await _friendshipRepository.UnblockUser(currentUserId, otherUserId);
                    break;
                
                default:
                    throw new ErrorModelException(ErrorCodes.IncorrectValue, nameof(FriendshipDTOActions));
            }
            
            return new OkObjectResult(new ResponseModel{
                Object = Mapper.Map<FriendshipModel>(result)
            });
        }

        [HttpGet("getallfriends"), ReallyAuthorize] //TODO: Temp endoint
        public async Task<IActionResult> GetAllFriends()
        {
            var friends = await _friendshipRepository.GetAllFriends(_auth.GetUserId());
            return new OkObjectResult(new ResponseModel{
                Object = Mapper.Map<List<FriendshipModel>>(friends)
            });
        }
    }
}