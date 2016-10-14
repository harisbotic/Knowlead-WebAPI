using System.Security.Claims;
using System.Threading.Tasks;
using Knowlead.BLL.Interfaces;
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
using Knowlead.DTO.LookupModels.Core;
using System.Collections.Generic;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Newtonsoft.Json.Linq;

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

        public async Task<ApplicationUser> GetUserByPrincipal(ClaimsPrincipal principal)
        {
            return await _userManager.GetUserAsync(principal);
        }

        public async Task<IActionResult> UpdateUserDetails(ApplicationUser applicationUser, JsonPatchDocument<ApplicationUserModel> applicationUserPatch)
        {
            var langs = _context.ApplicationUserLanguages
                                .Where(x => x.ApplicationUserId == applicationUser.Id)
                                .ToList();

            var langsModel = new List<LanguageModel>();

            foreach (var item in langs)
            {
                langsModel.Add(new LanguageModel{
                    CoreLookupId = item.LanguageId
                });
            }

            var applicationUserModel = Mapper.Map<ApplicationUserModel>(applicationUser);
            
            applicationUserModel.Languages = langsModel;


            applicationUserPatch.ApplyTo(applicationUserModel);

            var e = new ApplicationUserLanguage
            {
                LanguageId = 1,
                ApplicationUserId = applicationUser.Id
            };

            var varName = nameof(ApplicationUserModel.Languages);
            var varPath = $"/{varName}/";
            foreach (var operation in applicationUserPatch.Operations)
            {
                if(operation.path.StartsWith(varPath, StringComparison.CurrentCultureIgnoreCase))
                {
                    switch (operation.OperationType)
                    {
                        case OperationType.Add:
                            var @value = operation.value as JObject;
                            langs.Add(new ApplicationUserLanguage
                            {
                                LanguageId = (int)(@value.GetValue(nameof(LanguageModel.CoreLookupId), StringComparison.CurrentCultureIgnoreCase)),  
                                ApplicationUserId = applicationUser.Id
                            });
                        break;

                        case OperationType.Remove:
                            var index = int.Parse((operation.path.Substring(varPath.Length)));
                            langs.RemoveAt(index);
                        break;
                    }
                }
            }

            applicationUser.Name = applicationUserModel?.Name;
            applicationUser.Surname = applicationUserModel?.Surname;
            applicationUser.AboutMe = applicationUserModel?.AboutMe;
            applicationUser.Birthdate = applicationUserModel?.Birthdate;
            applicationUser.IsMale = applicationUserModel?.IsMale;

            applicationUser.CountryId = applicationUserModel?.CountryId;
            applicationUser.StateId = applicationUserModel?.StateId;
            applicationUser.MotherTongueId = applicationUserModel?.MotherTongueId;

            applicationUser.ApplicationUserLanguages = langs;

            var result = await _userManager.UpdateAsync(applicationUser);

            if(!result.Succeeded)
            {
                return new BadRequestObjectResult(new ResponseModel(result.Errors));
            }
            
            return new OkObjectResult(new ResponseModel{
                Object = applicationUserModel
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

        public async Task<IActionResult> SaveChangesAsync()
        {
            bool hasChanges = (await _context.SaveChangesAsync() > 0);

            if (!hasChanges)
            {
                var error = new ErrorModel(Constants.ErrorCodes.DatabaseError);
                return new BadRequestObjectResult(new ResponseModel(error));
            }

            return new OkObjectResult(new ResponseModel());
        }
    }
}