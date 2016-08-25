using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Knowlead.Common;
using Knowlead.DomainModel.UserModels;
using Knowlead.DTO;
using Knowlead.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using static Knowlead.Common.Constants;

namespace Knowlead.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller {

        UserManager<ApplicationUser> userManager;
        SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,
                SignInManager<ApplicationUser> signInManager) {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<ResponseModel> Register(ApplicationUserModel userModel) {
            ApplicationUser tmp = userModel.MapToApplicationUser();
            string password = userModel.Password;
            if (ModelState.IsValid) {
                IdentityResult result;
                try {
                    result = await userManager.CreateAsync(tmp, password);
                } catch(ArgumentException ex) {
                    return new ResponseModel{Success = false, ErrorMap = new Dictionary<string, List<ErrorModel>> {
                        {
                            ex.ParamName,
                            new List<ErrorModel>{new ErrorModel{ErrorCode = (int)ErrorCodes.DatabaseError, ErrorDescription = ex.Message}}
                        }
                    }};
                }
                return new ResponseModel{Success = result.Succeeded, ErrorList = result.Errors.AsStringList().MapToErrorList()};
            } else {
                return new ResponseModel{
                    Success = false,
                    ErrorMap = ModelState.AsDictionary().MapToErrorDictionary((int)ErrorCodes.ValidationError)
                };
            }
        }
    }
}