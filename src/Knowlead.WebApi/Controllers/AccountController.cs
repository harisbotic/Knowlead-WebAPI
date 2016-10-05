using System.Threading.Tasks;
using Knowlead.BLL.Interfaces;
using Knowlead.WebApi.Attributes;
using Knowlead.DomainModel.UserModels;
using Knowlead.DTO.ResponseModels;
using Knowlead.Common.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Knowlead.DTO.UserModels;
using AutoMapper;

namespace Knowlead.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : BaseController {
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 IAccountRepository accountRepository)
                                 : base(accountRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("register"), ValidateModel]
        public async Task<IActionResult> Register([FromBody] RegisterUserModel userModel)
        {
            return (await _accountRepository.RegisterApplicationUserAsync(userModel));
        }

        [HttpGet("me"), ReallyAuthorize]
        public async Task<IActionResult> me()
        {
            var applicationUser = await GetCurrentUserAsync();
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

        [HttpPatch("details"), ValidateModel, ReallyAuthorize]
        public async Task<IActionResult> Details([FromBody] JsonPatchDocument<ApplicationUserModel> userDetailsPatch)
        {
            var currentUser = (await GetCurrentUserAsync());  
            return (await _accountRepository.UpdateUserDetails(currentUser, userDetailsPatch));
        }

        [HttpPost("/account/login"), ValidateModelAttribute]
        public IActionResult dummy([FromBody] ConfirmEmailModel confirmEmailModel)
        {
            
            // _ResponseModel.AddFormError("code", "val", "par");
            // _ResponseModel.AddFormError("key", "val", "par");
            // _ResponseModel.AddFormError("key2", "val2", "par2");
            // _ResponseModel.AddErrors(ModelState);
            // _ResponseModel.AddError("hey");
            

            return Ok(new ResponseModel());
            
        }

        [HttpGet("/authorizetest"), ReallyAuthorize]
        public IActionResult AuthorizeTest()
        {
            return Ok(new ResponseModel());
        }
    }
}