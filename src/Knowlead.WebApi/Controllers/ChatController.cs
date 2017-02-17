using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Knowlead.Common.HttpRequestItems;
using Knowlead.BLL.Repositories.Interfaces;
using static Knowlead.Common.Constants;
using Knowlead.DTO.ResponseModels;
using Knowlead.DomainModel.ChatModels;
using Knowlead.DTO.ChatModels;
using static Knowlead.Common.Constants.EnumActions;
using AutoMapper;
using System.Collections.Generic;
using Knowlead.Common.Exceptions;
using Knowlead.Services.Interfaces;
using static Knowlead.Common.Constants.EnumStatuses;
using Microsoft.AspNetCore.Authorization;
using System;

namespace Knowlead.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = Policies.RegisteredUser)]
    public class ChatController : Controller
    {
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly Auth _auth;
        private readonly IChatServices _chatServices;

        public ChatController(IFriendshipRepository friendshipRepository, IChatServices chatServices,
                                          Auth auth)
        {
            _friendshipRepository = friendshipRepository;
            _chatServices = chatServices;
            _auth = auth;
        }

        [HttpPost("changefriendshipstatus")]
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

        [HttpGet("getallfriends")] //TODO: Temp endoint needs pagination
        public async Task<IActionResult> GetAllFriends(FriendshipStatus? status)
        {
            var friends = await _friendshipRepository.GetAllFriends(_auth.GetUserId(), status);
            return new OkObjectResult(new ResponseModel{
                Object = Mapper.Map<List<FriendshipModel>>(friends)
            });
        }

        [HttpPost("msg")]
        public async Task<IActionResult> SendChatMessage([FromBody] ChatMessageModel chatMessageModel)
        {
            var senderId = _auth.GetUserId();
            await _chatServices.SendChatMessage(chatMessageModel, senderId);
            
            return Ok(new ResponseModel());
        }

        [HttpGet("getConversation/{applicationUserId}")]
        public async Task<IActionResult> GetConversation([FromRoute] Guid applicationUserId, [FromQuery] string fromRowKey, [FromQuery] int numItems)
        {
            var currentUserId = _auth.GetUserId();
            var chatMessages = await _chatServices.GetConversation(currentUserId, applicationUserId, fromRowKey, numItems);
            
            return Ok(new ResponseModel{
                Object = Mapper.Map<List<ChatMessageModel>>(chatMessages)
            });
        }

        [HttpGet("getConversations")]
        public async Task<IActionResult> GetConversations([FromQuery] DateTimeOffset fromDateTime, [FromQuery] int numItems)
        {
            var currentUserId = _auth.GetUserId();
            var conversations = await _chatServices.GetConversations(currentUserId, fromDateTime, numItems);
            
            return Ok(new ResponseModel{
                Object = Mapper.Map<List<ConversationModel>>(conversations)
            });
        }
    }
}