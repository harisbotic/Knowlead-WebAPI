using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Knowlead.Common.HttpRequestItems;
using Knowlead.WebApi.Attributes;
using Knowlead.BLL.Repositories.Interfaces;
using System;
using Knowlead.BLL.Exceptions;
using static Knowlead.Common.Constants;
using Knowlead.DTO.ResponseModels;
using Knowlead.Common.Attributes;

namespace Knowlead.Controllers
{
    [Route("api/[controller]")]
    public class UserRelationshipController : Controller
    {
        private readonly IUserRelationshipRepository _userRelationshipRepository;
        private readonly Auth _auth;

        public UserRelationshipController(IUserRelationshipRepository userRelationshipRepository,
                                          Auth auth)
        {
            _userRelationshipRepository = userRelationshipRepository;
            _auth = auth;
        }

        [HttpPost("sendfriendrequest/{applicationUserId}"), ReallyAuthorize] // Because FromBody doesnt seem to work
        public async Task<IActionResult> FriendRequest(Guid applicationUserId)
        {
            var currentUserId = _auth.GetUserId();
            var success = await _userRelationshipRepository.SendFriendRequest(currentUserId, applicationUserId);
            
            if(!success)
                throw new ErrorModelException(ErrorCodes.DatabaseError);
            
            return new OkObjectResult(new ResponseModel());
        }
    }
}