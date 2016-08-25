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
        public async Task<ResponseModel> Register([FromBody] ApplicationUserModel userModel) {
            if (userModel == null) {
                return new ResponseModel(false, new ErrorModel("Model is empty", ErrorCodes.ValidationError));
            }
            ApplicationUser tmp = userModel.MapToApplicationUser();
            string password = userModel.Password;
            if (ModelState.IsValid) {
                IdentityResult result;
                try {
                    result = await userManager.CreateAsync(tmp, password);
                } catch(ArgumentException ex) {
                    return new ResponseModel(false, new Dictionary<string, List<ErrorModel>> {
                        {
                            ex.ParamName,
                            new List<ErrorModel>{new ErrorModel(ex.Message, ErrorCodes.DatabaseError)}
                        }
                    });
                }
                return new ResponseModel(result.Succeeded, result.Errors.MapToErrorList());
            } else {
                return new ResponseModel(false,
                    ModelState.AsDictionary().MapToErrorDictionary((int)ErrorCodes.ValidationError)
                );
            }
        }
    }
}