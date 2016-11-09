using System.Threading.Tasks;
using Knowlead.DAL;
using Knowlead.DomainModel.UserModels;
using Knowlead.DTO.ResponseModels;
using Knowlead.DTO.UserModels;
using Knowlead.Services;
using Microsoft.AspNetCore.Identity;
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.JsonPatch;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Knowlead.Common;
using System.Linq;
using System;
using System.Collections.Generic;
using Knowlead.BLL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Knowlead.BLL.Repositories
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

        public async Task<IActionResult> RegisterApplicationUserAsync(RegisterUserModel registerUserModel)
        {
            var applicationUser = Mapper.Map<ApplicationUser>(registerUserModel);
            
            applicationUser.UserName = registerUserModel.Email;
            var password = registerUserModel.Password;
            
            var result = await _userManager.CreateAsync(applicationUser, registerUserModel.Password);
            if (result.Succeeded)
            {
                string token = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
                string encodedEmail = WebUtility.UrlEncode(applicationUser.Email);
                string encodedToken = WebUtility.UrlEncode(token);
                string url = $"{_config["Urls:client"]}/confirmemail?email={encodedEmail}&code={encodedToken}";

                // message service probably needs try and catch but this is temp solution anyways
                await _messageServices.TempSendEmailAsync(applicationUser.Email,"Knowlead Confirmation", "haris.botic96@gmail.com", "KnowLead", url);
            }
            else
            {
                return new BadRequestObjectResult(new ResponseModel(result.Errors));
            }

            var applicationUserModel = Mapper.Map<ApplicationUserModel>(applicationUser);
            return new OkObjectResult(new ResponseModel{
                Object = applicationUserModel
            });
        }

        public async Task<IActionResult> UpdateUserDetails(ApplicationUser applicationUser, JsonPatchDocument<ApplicationUserModel> applicationUserPatch)
        {
            var applicationUserModel = Mapper.Map<ApplicationUserModel>(applicationUser);
            
            var forManualPatch = new Dictionary<string, Object>();
            forManualPatch.Add(nameof(ApplicationUserModel.Languages), applicationUser.ApplicationUserLanguages);
            forManualPatch.Add(nameof(ApplicationUserModel.Interests), applicationUser.ApplicationUserInterests);
            applicationUserPatch.CustomApplyTo(applicationUserModel, forManualPatch, applicationUser);
            
            if(applicationUserModel.StateId != null && applicationUserModel.StateId != applicationUser?.StateId)
            {
                var newState = _context.States
                                    .Where(x => x.GeoLookupId == applicationUserModel.StateId)
                                    .FirstOrDefault();

                if(newState == null)
                    return new BadRequestObjectResult(new ResponseModel(new FormErrorModel(nameof(ApplicationUserModel.StateId), Constants.ErrorCodes.IncorrectValue)));

                applicationUserModel.CountryId = newState.StatesCountryId;
            }

            applicationUser = Mapper.Map<ApplicationUserModel, ApplicationUser>(applicationUserModel, applicationUser);

            if(String.IsNullOrEmpty(applicationUser.Name))
                    return new BadRequestObjectResult(new ResponseModel(new FormErrorModel(nameof(ApplicationUserModel.Name), Constants.ErrorCodes.RequiredField)));
            
            if(String.IsNullOrEmpty(applicationUser.Surname))
                    return new BadRequestObjectResult(new ResponseModel(new FormErrorModel(nameof(ApplicationUserModel.Surname), Constants.ErrorCodes.RequiredField)));
            
            
            var result = await _userManager.UpdateAsync(applicationUser);
            
            if(!result.Succeeded)
            {
                return new BadRequestObjectResult(new ResponseModel(result.Errors));
            }
            
            var updatedUser = Mapper.Map<ApplicationUserModel>(await GetApplicationUserById(applicationUser.Id, true));

            return new OkObjectResult(new ResponseModel{
                Object = updatedUser
            });
            
        }

        public async Task<IActionResult> ConfirmEmail(ConfirmEmailModel confirmEmailModel)
        {
            var user = await _userManager.FindByEmailAsync(confirmEmailModel.Email);
            if (user == null)
            {
                var formError = new FormErrorModel(nameof(confirmEmailModel.Email), Constants.ErrorCodes.UserNotFound);
                return new BadRequestObjectResult(new ResponseModel(formError));
            }

            var correctPassword = await _userManager.CheckPasswordAsync(user, confirmEmailModel.Password);
            if(!correctPassword)
            {
                var formError = new FormErrorModel(nameof(confirmEmailModel.Email), Constants.ErrorCodes.PasswordIncorrect);
                return new BadRequestObjectResult(new ResponseModel(formError));
            }

            var result = await _userManager.ConfirmEmailAsync(user, confirmEmailModel.Code);
            if(!result.Succeeded)
            {
                var formError = new FormErrorModel(nameof(confirmEmailModel.Code), Constants.ErrorCodes.ConfirmationCodeIncorrect);
                return new BadRequestObjectResult(new ResponseModel(formError));
            }

            return new OkObjectResult(new ResponseModel());
        }

        public async Task<ApplicationUser> GetApplicationUserById(Guid userId, bool includeDetails = false)
        {
            IQueryable<ApplicationUser> userQuery = _context.ApplicationUsers;
                    
            if(includeDetails)
                userQuery = userQuery.Include(x => x.ApplicationUserLanguages)
                                        .ThenInclude(x => x.Language)
                                        .Include(x => x.ApplicationUserInterests)
                                        .ThenInclude(x => x.Fos)
                                        .Include(x => x.MotherTongue)
                                        .Include(x => x.Country)
                                        .Include(x => x.State);

                userQuery.Where(x => x.Id == userId);

            return await userQuery.FirstOrDefaultAsync();
        }
    }
}