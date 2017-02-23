using System.Threading.Tasks;
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
using Knowlead.Common.Exceptions;
using System.Collections.Generic;

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
        [AllowAnonymous]
        public async Task<IActionResult> GetApplicationUserById(Guid userId, bool includeDetails = true)
        {
            var user = await _accountRepository.GetApplicationUserById(userId, includeDetails);
            if(user == null)
                return BadRequest(new ResponseModel(new ErrorModel(ErrorCodes.EntityNotFound)));
            
            return Ok(new ResponseModel {
                Object = Mapper.Map<ApplicationUserModel>(user)
            });

        }

        [HttpPost("register"), ValidateModel]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterUserModel userModel)
        {
            return (await _accountRepository.RegisterApplicationUserAsync(userModel));
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> Me()
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

        [HttpPost("generatePasswordResetToken/{email}"), ValidateModel]
        [AllowAnonymous]
        public async Task<IActionResult> GeneratePasswordResetToken([FromRouteAttribute] string email)
        {
            var result = await _accountRepository.GeneratePasswordResetTokenAsync(email);

            if(result)
                return Ok(new ResponseModel());

            return BadRequest(new ResponseModel());
        }

        [HttpPost("resetPassword"), ValidateModel]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] PasswordResetModel passwordResetModel)
        {
            var result = await _accountRepository.ResetPasswordAsync(passwordResetModel);

            if(result)
                return Ok(new ResponseModel());

            return BadRequest(new ResponseModel());
        }

        [HttpPost("details"), ValidateModel]
        [Authorize]
        public async Task<IActionResult> Details([FromBody] JsonPatchDocument<ApplicationUserModel> userDetailsPatch)
        {
            var currentUser = await _auth.GetUser(true);

            return (await _accountRepository.UpdateUserDetails(currentUser, userDetailsPatch));
        }

        [HttpPost("changeProfilePicture/{imageBlobId}")]
        [Authorize]
        public async Task<IActionResult> ChangeProfilePicture(Guid imageBlobId)
        {
            var applicationUser = await _auth.GetUser(false);

            applicationUser = await _accountRepository.ChangeProfilePicture(imageBlobId, applicationUser);

            return Ok(new ResponseModel{
                Object = Mapper.Map<ApplicationUserModel>(applicationUser)
            });
        }

        [HttpDelete("removeProfilePicture")]
        [Authorize]
        public async Task<IActionResult> RemoveProfilePicture()
        {
            var applicationUser = await _auth.GetUser(false);

            applicationUser = await _accountRepository.RemoveProfilePicture(applicationUser);

            return Ok(new ResponseModel{
                Object = Mapper.Map<ApplicationUserModel>(applicationUser)
            });
        }

        [HttpGet("search")]
        [Authorize]
        public async Task<IActionResult> Search([FromQuery] string q)
        {
            var applicationUserId = _auth.GetUserId();

            var searchResult = await _accountRepository.Search(q, applicationUserId);

            return Ok(new ResponseModel{
                Object = Mapper.Map<List<ApplicationUserModel>>(searchResult)
            });
        }
    }
}