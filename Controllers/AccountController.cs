using System.Threading.Tasks;
using Knowlead.BLL.Interfaces;
using Knowlead.Common.Attributes;
using Knowlead.DomainModel.UserModels;
using Knowlead.DTO;
using Knowlead.DTO.Mappers;
using Knowlead.Utils;
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

        [HttpGet("me"), ReallyAuthorize]
        public async Task<ApplicationUserModel> me()
        {
            return (await GetCurrentUser()).MapToApplicationUserModel();
        }

        [HttpGetAttribute("/account/login")]
        public IActionResult dummy()
        {
            return new ForbidResult();
        }

        [HttpGetAttribute("authorizetest"), ReallyAuthorize]
        public ResponseModel AuthorizeTest()
        {
            return new ResponseModel(true);
        }
    }
}