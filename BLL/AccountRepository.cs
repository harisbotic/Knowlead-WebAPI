using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Knowlead.BLL.Interfaces;
using Knowlead.DomainModel;
using Knowlead.DomainModel.UserModels;
using Knowlead.DTO;
using Knowlead.DTO.ApplicationUserModels;
using Knowlead.DTO.Mappers;
using Knowlead.Services;
using Microsoft.AspNetCore.Identity;
using static Knowlead.Common.Constants;
using System.Net;
using Microsoft.Extensions.Configuration;

namespace Knowlead.BLL
{
    public class AccountRepository : IAccountRepository
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private MessageServices _messageServices;
        private IConfigurationRoot _config;

        public AccountRepository(ApplicationDbContext context,
                UserManager<ApplicationUser> userManager,
                SignInManager<ApplicationUser> signInManager,
                MessageServices messageServices,
                IConfigurationRoot config)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _messageServices = messageServices;
            _config = config;
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
                    string encodedToken = WebUtility.UrlEncode(token);
                    string encodedEmail = WebUtility.UrlEncode(applicationUser.Email);
                    string url = $"{_config["Urls:api"]}/api/account/confirmemail?userId={encodedEmail}&code={encodedToken}";
                    
                    url = $"{token}"; //TEMP, WHILE WE DONT HAVE CLIENT APP

                    await _messageServices.TempSendEmailAsync(applicationUser.Email,"Knowlead Confirmation", "haris.botic96@gmail.com", "KnowLead", url);
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

        public async Task<ResponseModel> UpdateUserDetails(ApplicationUser applicationUser, UserDetailsModel userDetailsModel)
        {
            IdentityResult result;

            applicationUser.AboutMe = userDetailsModel.AboutMe;

            result = await _userManager.UpdateAsync(applicationUser);

            return new ResponseModel(result.Succeeded, result.Errors.MapToErrorList());
        }

        public async Task<ResponseModel> ConfirmEmail(ConfirmEmailModel confirmEmailModel)
        {
            var user = await _userManager.FindByEmailAsync(confirmEmailModel.Email);
            if (user == null)
                return new ResponseModel(false, new ErrorModel("User with specified email doesn't exist", ErrorCodes.DatabaseError));

            var correctPassword = await _userManager.CheckPasswordAsync(user, confirmEmailModel.Password);
            if(!correctPassword)
                return new ResponseModel(false, new ErrorModel("Incorrect Password", ErrorCodes.ValidationError));

            var result = await _userManager.ConfirmEmailAsync(user, confirmEmailModel.Code);
            if(result.Succeeded)
                return new ResponseModel(result.Succeeded, result.Errors.MapToErrorList());

            return new ResponseModel(false, new ErrorModel("Code doesn't match", ErrorCodes.ValidationError));
        }

        public async Task<ResponseModel> SaveChangesAsync()
        {
            bool hasChanges = (await _context.SaveChangesAsync() > 0);

            if (!hasChanges)
            {
                return new ResponseModel(false, new ErrorModel("0 Changes were made", ErrorCodes.DatabaseError));
            }

            return new ResponseModel(true);
        }
    }
}