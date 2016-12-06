using System.Threading.Tasks;
using Knowlead.WebApi.Attributes;
using Knowlead.DTO.ResponseModels;
using Knowlead.Common.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Knowlead.DTO.UserModels;
using AutoMapper;
using static Knowlead.Common.Constants;
using Knowlead.Common.HttpRequestItems;
using Knowlead.BLL.Repositories.Interfaces;
using System;

namespace Knowlead.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller {
        private readonly IAccountRepository _accountRepository;
        private readonly Auth _auth;

        public AccountController(IAccountRepository accountRepository,
                                 Auth auth)
        {
            _accountRepository = accountRepository;
            _auth = auth;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetApplicationUserById(Guid userId)
        {
            var user = await _accountRepository.GetApplicationUserById(userId, true);
            if(user == null)
                return BadRequest(new ResponseModel(new ErrorModel(ErrorCodes.EntityNotFound)));
            
            return Ok(new ResponseModel {
                Object = user
            });

        }

        [HttpPost("register"), ValidateModel]
        public async Task<IActionResult> Register([FromBody] RegisterUserModel userModel)
        {
            return (await _accountRepository.RegisterApplicationUserAsync(userModel));
        }

        [HttpGet("me"), ReallyAuthorize]
        public async Task<IActionResult> me()
        {
            var applicationUser = await _auth.GetUser(true);
            var applicationUserModel = Mapper.Map<ApplicationUserModel>(applicationUser);
            
            return Ok(new ResponseModel{
                Object = applicationUserModel
            });
        }

        [HttpPost("confirmEmail"), ValidateModel]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmEmailModel confirmEmailModel)
        {
            return (await _accountRepository.ConfirmEmail(confirmEmailModel));
        }

        [HttpPatch("details"), ReallyAuthorize, ValidateModel]
        public async Task<IActionResult> Details([FromBody] JsonPatchDocument<ApplicationUserModel> userDetailsPatch)
        {
            var currentUser = await _auth.GetUser(true);

            return (await _accountRepository.UpdateUserDetails(currentUser, userDetailsPatch));
        }
    }
}