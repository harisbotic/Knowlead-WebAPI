using System.Threading.Tasks;
using Knowlead.BLL.Interfaces;
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

        [HttpGet("me"), Authorize]
        public async Task<ApplicationUserModel> me()
        {
            return (await GetCurrentUser()).MapToApplicationUserModel();
        }

        [HttpGetAttribute("/account/login")]
        public IActionResult dummy()
        {
            return Forbid("Not logged in");
        }
    }
}