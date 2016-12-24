// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Knowlead.Common.HttpRequestItems;
// using Knowlead.WebApi.Attributes;
// using Knowlead.BLL.Repositories.Interfaces;
// using System;
// using Knowlead.BLL.Exceptions;
// using static Knowlead.Common.Constants;
// using Knowlead.DTO.ResponseModels;

// namespace Knowlead.Controllers
// {
//     [Route("api/[controller]")]
//     public class UserRelationshipController : Controller
//     {
//         private readonly IUserRelationshipRepository _userRelationshipRepository;
//         private readonly Auth _auth;

//         public UserRelationshipController(IUserRelationshipRepository userRelationshipRepository,
//                                           Auth auth)
//         {
//             _userRelationshipRepository = userRelationshipRepository;
//             _auth = auth;
//         }

//         [HttpPost("sendfriendrequest/{applicationUserId}"), ReallyAuthorize] // Because FromBody doesnt seem to work
//         public async Task<IActionResult> SendFriendRequest(Guid applicationUserId)
//         {
//             var currentUserId = _auth.GetUserId();
//             var success = await _userRelationshipRepository.SendFriendRequest(currentUserId, applicationUserId);
            
//             if(!success)
//                 throw new ErrorModelException(ErrorCodes.DatabaseError);
            
//             return new OkObjectResult(new ResponseModel());
//         }

//         [HttpPost("acceptfriendrequest/{applicationUserId}"), ReallyAuthorize] // Because FromBody doesnt seem to work
//         public async Task<IActionResult> AcceptFriendRequest(Guid applicationUserId)
//         {
//             var currentUserId = _auth.GetUserId();
//             var success = await _userRelationshipRepository.AcceptFriendRequest(currentUserId, applicationUserId);
            
//             if(!success)
//                 throw new ErrorModelException(ErrorCodes.DatabaseError);
            
//             return new OkObjectResult(new ResponseModel());
//         }

//         [HttpPost("declinefriendrequest/{applicationUserId}"), ReallyAuthorize] // Because FromBody doesnt seem to work
//         public async Task<IActionResult> DeclineFriendRequest(Guid applicationUserId)
//         {
//             var currentUserId = _auth.GetUserId();
//             var success = await _userRelationshipRepository.DeclineFriendRequest(currentUserId, applicationUserId);
            
//             if(!success)
//                 throw new ErrorModelException(ErrorCodes.DatabaseError);
            
//             return new OkObjectResult(new ResponseModel());
//         }

//         [HttpPost("removefriend/{applicationUserId}"), ReallyAuthorize] // Because FromBody doesnt seem to work
//         public async Task<IActionResult> RemoveFriend(Guid applicationUserId)
//         {
//             var currentUserId = _auth.GetUserId();
//             var success = await _userRelationshipRepository.RemoveFriend(currentUserId, applicationUserId);
            
//             if(!success)
//                 throw new ErrorModelException(ErrorCodes.DatabaseError);
            
//             return new OkObjectResult(new ResponseModel());
//         }

//         [HttpPost("blockuser/{applicationUserId}"), ReallyAuthorize] // Because FromBody doesnt seem to work
//         public async Task<IActionResult> BlockUser(Guid applicationUserId)
//         {
//             var currentUserId = _auth.GetUserId();
//             var success = await _userRelationshipRepository.BlockUser(currentUserId, applicationUserId);
            
//             if(!success)
//                 throw new ErrorModelException(ErrorCodes.DatabaseError);
            
//             return new OkObjectResult(new ResponseModel());
//         }
//     }
// }