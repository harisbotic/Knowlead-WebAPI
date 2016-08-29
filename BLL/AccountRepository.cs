using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Knowlead.BLL.Interfaces;
using Knowlead.DomainModel;
using Knowlead.DomainModel.UserModels;
using Knowlead.DTO;
using Knowlead.DTO.Mappers;
using Microsoft.AspNetCore.Identity;
using static Knowlead.Common.Constants;

namespace Knowlead.BLL
{
    public class AccountRepository : IAccountRepository
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public AccountRepository(ApplicationDbContext context,
                UserManager<ApplicationUser> userManager,
                SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ResponseModel> RegisterApplicationUserAsync(RegisterUserModel applicationUserModel)
        {
            var applicationUser = applicationUserModel.MapToApplicationUser();
            var password = applicationUserModel.Password;
            IdentityResult result;
            
            try 
            {
                result = await _userManager.CreateAsync(applicationUser, applicationUserModel.Password);
                if (result.Succeeded) {
                    string token = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
                    //TODO: send email
                }
            } 
            catch(ArgumentException ex) 
            {
                return new ResponseModel(false, new Dictionary<string, List<ErrorModel>> 
                {
                    {
                        ex.ParamName,
                        new List<ErrorModel>{new ErrorModel(ex.Message, ErrorCodes.DatabaseError)}
                    }
                });
            }

            return new ResponseModel(result.Succeeded, result.Errors.MapToErrorList());
        }

        public async Task<ApplicationUser> GetUserByPrincipal(ClaimsPrincipal principal)
        {
            return await _userManager.GetUserAsync(principal);
        }
    }
}