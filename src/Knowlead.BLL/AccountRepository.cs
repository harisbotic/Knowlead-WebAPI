using System;
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
            IdentityResult result;
            
            try 
            {
                result = await _userManager.CreateAsync(applicationUser, registerUserModel.Password);
                if (result.Succeeded) {
                    string token = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
                    string encodedEmail = WebUtility.UrlEncode(applicationUser.Email);
                    string encodedToken = WebUtility.UrlEncode(token);
                    string url = $"{_config["Urls:client"]}/confirmemail?email={encodedEmail}&code={encodedToken}";

                    await _messageServices.TempSendEmailAsync(applicationUser.Email,"Knowlead Confirmation", "haris.botic96@gmail.com", "KnowLead", url);
                }
            } 
            catch(ArgumentException ex) 
            {
                return new BadRequestObjectResult(new ResponseModel(new FormErrorModel
                {
                    Key = ex.ParamName,
                    Value = ex.Message
                }));
                
            }

            return new OkObjectResult(new ResponseModel());
        }

        public async Task<ApplicationUser> GetUserByPrincipal(ClaimsPrincipal principal)
        {
            return await _userManager.GetUserAsync(principal);
        }

        public async Task<IActionResult> UpdateUserDetails(ApplicationUser applicationUser, JsonPatchDocument<ApplicationUserModel> applicationUserPatch)
        {
            IdentityResult result;

            var applicationUserModel = Mapper.Map<ApplicationUserModel>(applicationUser);
            applicationUserPatch.ApplyTo(applicationUserModel);

            applicationUser.Name = applicationUserModel?.Name;
            applicationUser.Surname = applicationUserModel?.Surname;
            applicationUser.AboutMe = applicationUserModel?.AboutMe;
            applicationUser.Birthdate = applicationUserModel?.Birthdate;
            applicationUser.IsMale = applicationUserModel?.IsMale;

            applicationUser.CountryId = applicationUserModel?.CountryId;
            applicationUser.StateId = applicationUserModel?.StateId;

            // List<EmpAssets> oldAssests = context.EmpAssets.Where(x => x.EmployeeId == employeeId).ToList();

            // List<EmpAssets> addedAssests = updatedAssests.ExceptBy(oldAssests, x => x.CityId).ToList();
            // List<EmpAssets> deletedAssests = oldAssests.ExceptBy(updatedAssests, x => x.CityId).ToList();

            // deletedAssests.ForEach( x => context.Entry(x).State = EntityState.Deleted);
            // addedAssests.ForEach(x => context.Entry(x).State = EntityState.Added);

            result = await _userManager.UpdateAsync(applicationUser);

            if(!result.Succeeded)
            {
                return new BadRequestObjectResult(new ResponseModel(result.Errors));
            }
            
            return new OkObjectResult(new ResponseModel());
            
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