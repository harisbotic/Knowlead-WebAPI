using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Knowlead.Common.HttpRequestItems;
using Knowlead.WebApi.Attributes;
using Knowlead.BLL.Repositories.Interfaces;
using Knowlead.BLL.Exceptions;
using static Knowlead.Common.Constants;
using Knowlead.DTO.ResponseModels;
using Knowlead.DomainModel.UserModels;
using Knowlead.DTO.ChatModels;
using static Knowlead.Common.Constants.EnumActions;

namespace Knowlead.Controllers
{
    [Route("api/[controller]")]
    public class ChatController : Controller
    {
        private readonly IUserRelationshipRepository _userRelationshipRepository;
        private readonly Auth _auth;

        public ChatController(IUserRelationshipRepository userRelationshipRepository,
                                          Auth auth)
        {
            _userRelationshipRepository = userRelationshipRepository;
            _auth = auth;
        }

        [HttpPost("changefriendshipstatus"), ReallyAuthorize]
        public async Task<IActionResult> ChangeFriendshipStatus([FromBody] ChangeFriendshipStatusModel cfsm)
        {
            var currentUserId = _auth.GetUserId();
            var otherUserId = cfsm.ApplicationUserId;
            
            var action = cfsm.Action;
            ApplicationUserRelationship result = null;
            switch (action) 
            {
                case(FriendshipDTOActions.NewRequest):
                    result = await _userRelationshipRepository.SendFriendRequest(currentUserId, otherUserId);
                    break;
                
                case(FriendshipDTOActions.AcceptRequest):
                    result = await _userRelationshipRepository.RespondToFriendRequest(currentUserId, otherUserId, true);
                    break;
                
                case(FriendshipDTOActions.DeclineRequest):
                    result = await _userRelationshipRepository.RespondToFriendRequest(currentUserId, otherUserId, false);
                    break;

                case(FriendshipDTOActions.CancelRequest):
                    result = await _userRelationshipRepository.CancelFriendRequest(currentUserId, otherUserId);
                    break;

                case(FriendshipDTOActions.RemoveFriend):
                    result = await _userRelationshipRepository.RemoveFriend(currentUserId, otherUserId);
                    break;

                case(FriendshipDTOActions.BlockUser):
                    result = await _userRelationshipRepository.BlockUser(currentUserId, otherUserId);
                    break;

                case(FriendshipDTOActions.UnblockUser):
                    result = await _userRelationshipRepository.UnblockUser(currentUserId, otherUserId);
                    break;
                
                default:
                    throw new ErrorModelException(ErrorCodes.IncorrectValue, nameof(FriendshipDTOActions));
            }
            
            return new OkObjectResult(new ResponseModel{
                Object = result
            });
        }

        [HttpGet("list"), ReallyAuthorize]
        public async Task<IActionResult> List()
        {
            var friends = await _userRelationshipRepository.GetAllFriends(_auth.GetUserId());
            return new OkObjectResult(new ResponseModel{
                Object = friends
            });
        }
    }
}