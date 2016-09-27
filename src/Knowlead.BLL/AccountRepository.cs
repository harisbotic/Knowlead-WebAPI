using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Knowlead.BLL.Interfaces;
using Knowlead.DAL;
using Knowlead.DomainModel.UserModels;
using Knowlead.DTO.SpecificModels.ApplicationUserModels;
using Knowlead.DTO.SpecificModels;
using Knowlead.DTO.DomainModels.ApplicationUserModels;
using Knowlead.DTO.Mappings;
using Knowlead.Services;
using Microsoft.AspNetCore.Identity;
using static Knowlead.Common.Constants;
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.JsonPatch;

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
                    string encodedEmail = WebUtility.UrlEncode(applicationUser.Email);
                    string encodedToken = WebUtility.UrlEncode(token);
                    string url = $"{_config["Urls:client"]}/confirmemail?email={encodedEmail}&code={encodedToken}";

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

        public async Task<ResponseModel> UpdateUserDetails(ApplicationUser applicationUser, JsonPatchDocument<UserDetailsModel> userDetailsPatch)
        {
            IdentityResult result;

            var userDetailsModel = applicationUser.MapToUserDetailModel();
            userDetailsPatch.ApplyTo(userDetailsModel);

            applicationUser.Name = userDetailsModel?.Name;
            applicationUser.Surname = userDetailsModel?.Surname;
            applicationUser.AboutMe = userDetailsModel?.AboutMe;
            applicationUser.Birthdate = userDetailsModel?.Birthdate;
            applicationUser.IsMale = userDetailsModel?.IsMale;

            applicationUser.CountryId = userDetailsModel?.CountryId;
            applicationUser.StateId = userDetailsModel?.StateId;

            // List<EmpAssets> oldAssests = context.EmpAssets.Where(x => x.EmployeeId == employeeId).ToList();

            // List<EmpAssets> addedAssests = updatedAssests.ExceptBy(oldAssests, x => x.CityId).ToList();
            // List<EmpAssets> deletedAssests = oldAssests.ExceptBy(updatedAssests, x => x.CityId).ToList();

            // deletedAssests.ForEach( x => context.Entry(x).State = EntityState.Deleted);
            // addedAssests.ForEach(x => context.Entry(x).State = EntityState.Added);

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