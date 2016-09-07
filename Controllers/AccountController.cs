using System.Threading.Tasks;
using Knowlead.BLL.Interfaces;
using Knowlead.Common.Attributes;
using Knowlead.DomainModel.UserModels;
using Knowlead.DTO;
using Knowlead.DTO.ApplicationUserModels;
using Knowlead.DTO.Mappers;
using Knowloead.Common.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ResponseModel> Register([FromBody] RegisterUserModel userModel)
        {
            return (await _accountRepository.RegisterApplicationUserAsync(userModel));
        }

        [HttpGet("me"), ReallyAuthorize(false)]
        public async Task<ApplicationUserModel> me()
        {
            return (await GetCurrentUser()).MapToApplicationUserModel();
        }

        [HttpPost("confirmEmail"), ValidateModel]
        [AllowAnonymous]
        public async Task<ResponseModel> ConfirmEmail([FromBody] ConfirmEmailModel confirmEmailModel)
        {
            return (await _accountRepository.ConfirmEmail(confirmEmailModel));
        }

        [HttpPatch("details"), ValidateModel]
        public async Task<ResponseModel> Details([FromBody] UserDetailsModel userDetailsModel)
        {
            var currentUser = await GetCurrentUser();
            return (await _accountRepository.UpdateUserDetails(currentUser, userDetailsModel));
        }

        [HttpGet("/account/login")]
        public IActionResult dummy()
        {
            return new ForbidResult();
        }

        [HttpGet("authorizetest"), ReallyAuthorize]
        public ResponseModel AuthorizeTest()
        {
            return new ResponseModel(true);
        }
    }
}