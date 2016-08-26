using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Knowlead.BLL;
using Knowlead.BLL.Interfaces;
using Knowlead.Common;
using Knowlead.DomainModel.UserModels;
using Knowlead.DTO;
using Knowlead.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static Knowlead.Common.Constants;

namespace Knowlead.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller {
        UserManager<ApplicationUser> _userManager;
        SignInManager<ApplicationUser> _signInManager;
        IAccountRepository _accountRepository;

        public AccountController(UserManager<ApplicationUser> userManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 IAccountRepository accountRepository) 
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _accountRepository = accountRepository;
        }

        [HttpPost("register")]
        public async Task<ResponseModel> Register([FromBody] ApplicationUserModel userModel)
        {
            if (userModel == null) 
                return new ResponseModel(false, new ErrorModel("Model is empty", ErrorCodes.ValidationError));
                
            if (!ModelState.IsValid)
            {
                return new ResponseModel(false,
                    ModelState.AsDictionary().MapToErrorDictionary((int)ErrorCodes.ValidationError)
                );
            }

            return (await _accountRepository.RegisterApplicationUserAsync(userModel));
        }
    }
}