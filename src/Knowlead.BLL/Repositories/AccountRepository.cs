using System.Threading.Tasks;
using Knowlead.DAL;
using Knowlead.DomainModel.UserModels;
using Knowlead.DTO.ResponseModels;
using Knowlead.DTO.UserModels;
using Knowlead.Services;
using Microsoft.AspNetCore.Identity;
using System.Net;
using Microsoft.AspNetCore.JsonPatch;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using static Knowlead.Common.Constants;
using System.Linq;
using System;
using System.Collections.Generic;
using Knowlead.BLL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Knowlead.Common.Exceptions;
using static Knowlead.Common.Constants.EnumStatuses;
using Knowlead.Services.Interfaces;
using Microsoft.Extensions.Options;
using Knowlead.Common.Configurations.AppSettings;
using Knowlead.BLL.Emails;

namespace Knowlead.BLL.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly MessageServices _messageServices;
        private readonly AppSettings _appSettings;
        private readonly ITransactionServices _transactionServices;

        public AccountRepository(ApplicationDbContext context,
                UserManager<ApplicationUser> userManager,
                MessageServices messageServices,
                IOptions<AppSettings> appSettings,
                ITransactionServices transactionServices)
        {
            _context = context;
            _userManager = userManager;
            _messageServices = messageServices;
            _appSettings = appSettings.Value;
            _transactionServices = transactionServices;
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

                RegistrationEmail emailData = new RegistrationEmail{
                    RegistrationLink = $"{_appSettings.BaseUrls.WebClient}/confirmemail?email={encodedEmail}&code={encodedToken}"
                };

                await _messageServices.SendEmailAsync(applicationUser.Email, "Knowlead Email Confirmation", emailData);
            }
            else
            {
                return new BadRequestObjectResult(new ResponseModel(result.Errors));
            }

            if(registerUserModel.ReferralUserId != null)
            {
                var referral = new ApplicationUserReferral(applicationUser.Id, registerUserModel.ReferralUserId.GetValueOrDefault());
                _context.ApplicationUserReferrals.Add(referral);
                await _context.SaveChangesAsync();
            }
            await _transactionServices.RewardMinutes(applicationUser.Id, 100, 0, "START");
            var applicationUserModel = Mapper.Map<ApplicationUserModel>(applicationUser);
            return new OkObjectResult(new ResponseModel{
                Object = applicationUserModel
            });
        }

        public async Task<bool> GeneratePasswordResetTokenAsync(string email)
        {
            var applicationUser = await _userManager.FindByEmailAsync(email);
            
            if(applicationUser == null)
                throw new ErrorModelException(ErrorCodes.EmailInvalid);

            string token = await _userManager.GeneratePasswordResetTokenAsync(applicationUser);
            string encodedEmail = WebUtility.UrlEncode(applicationUser.Email);
            string encodedToken = WebUtility.UrlEncode(token);

            PasswordResetEmail emailData = new PasswordResetEmail {
                PwdResetLink = $"{_appSettings.BaseUrls.WebClient}/resetpassword?email={encodedEmail}&token={encodedToken}"
            };

            return await _messageServices.SendEmailAsync(applicationUser.Email, "Knowlead Password Reset", emailData);
        }

        public async Task<bool> ResetPasswordAsync(PasswordResetModel passwordResetModel)
        {
            var email = passwordResetModel.Email;
            var token = passwordResetModel.Token;
            var newPassword = passwordResetModel.NewPassword;
            
            var applicationUser = await _userManager.FindByEmailAsync(email);
            
            if(applicationUser == null)
                throw new ErrorModelException(ErrorCodes.EmailInvalid);

            var result = await _userManager.ResetPasswordAsync(applicationUser, token, newPassword);
            if (result.Succeeded)
            {
                return true;
            }
            else
            { 
                throw new FormErrorModelException(nameof(PasswordResetModel.NewPassword), result.Errors.First().Description); //TODO: Code is fieldName and Descrition is actually the code, I would remove description property and leave code only
            }
        }

        public async Task<List<ApplicationUserReferral>> GetReferrals(Guid applicationUser, bool registrated = true)
        {
            return await _context.ApplicationUserReferrals.Where(x => x.ReferralUserId.Equals(applicationUser) && x.UserRegistred == registrated).Include(x => x.NewRegistredUser).ToListAsync();
        }
        public async Task<int> GetReferralsCount(Guid applicationUser, bool registrated = true)
        {
            return await _context.ApplicationUserReferrals.Where(x => x.ReferralUserId.Equals(applicationUser) && x.UserRegistred == registrated).CountAsync();
        }

        public async Task<IActionResult> UpdateUserDetails(ApplicationUser applicationUser, JsonPatchDocument<ApplicationUserModel> applicationUserPatch)
        {
            var applicationUserModel = Mapper.Map<ApplicationUserModel>(applicationUser);
            
            var forManualPatch = new Dictionary<string, Object>();
            forManualPatch.Add(nameof(ApplicationUserModel.Languages), applicationUser.ApplicationUserLanguages);
            forManualPatch.Add(nameof(ApplicationUserModel.Interests), applicationUser.ApplicationUserInterests);
            applicationUserPatch.CustomApplyTo(_context, applicationUserModel, forManualPatch, applicationUser);

            //TODO: These checkings should be in service, or another place ment for ApplicationUserValidations
            if(applicationUserModel.ProfilePictureId != null && !applicationUserModel.ProfilePictureId.Equals(applicationUser?.ProfilePictureId))
            {
                var imageBlob = _context.ImageBlobs
                                    .Where(x => x.BlobId == applicationUserModel.ProfilePictureId)
                                    .FirstOrDefault();

                if(imageBlob == null)
                    return new BadRequestObjectResult(new ResponseModel(new FormErrorModel(nameof(ApplicationUserModel.ProfilePictureId), ErrorCodes.IncorrectValue)));
            }

            if(applicationUserModel.StateId != null && applicationUserModel.StateId != applicationUser?.StateId)
            {
                var newState = _context.States
                                    .Where(x => x.GeoLookupId == applicationUserModel.StateId)
                                    .FirstOrDefault();

                if(newState == null)
                    return new BadRequestObjectResult(new ResponseModel(new FormErrorModel(nameof(ApplicationUserModel.StateId), ErrorCodes.IncorrectValue)));

                applicationUserModel.CountryId = newState.StatesCountryId;
            }

            applicationUser = Mapper.Map<ApplicationUserModel, ApplicationUser>(applicationUserModel, applicationUser);

            if(String.IsNullOrEmpty(applicationUser.Name))
                    return new BadRequestObjectResult(new ResponseModel(new FormErrorModel(nameof(ApplicationUserModel.Name), ErrorCodes.RequiredField)));
            
            if(String.IsNullOrEmpty(applicationUser.Surname))
                    return new BadRequestObjectResult(new ResponseModel(new FormErrorModel(nameof(ApplicationUserModel.Surname), ErrorCodes.RequiredField)));
            
            if(applicationUser.Birthdate.HasValue)
            if(DateTime.UtcNow.Year - applicationUser.Birthdate.GetValueOrDefault().Year < 6)
                     return new BadRequestObjectResult(new ResponseModel(new FormErrorModel(nameof(ApplicationUserModel.Birthdate), ErrorCodes.AgeTooYoung, "6")));

            else if(DateTime.UtcNow.Year - applicationUser.Birthdate.GetValueOrDefault().Year > 99)
                     return new BadRequestObjectResult(new ResponseModel(new FormErrorModel(nameof(ApplicationUserModel.Birthdate), ErrorCodes.AgeTooOld, "99")));

            var result = await _userManager.UpdateAsync(applicationUser);
            /* TEMP CODE */
            var referral = await _context.ApplicationUserReferrals.Where(x => x.NewRegistredUserId.Equals(applicationUser.Id)).FirstOrDefaultAsync();
            
            if(referral != null && !referral.UserRegistred)
            {
                referral.UserRegistred = true;
                _context.Update(referral);
                await _context.SaveChangesAsync();
            }
            /* TEMP CODE */
            if(!result.Succeeded)
            {
                return new BadRequestObjectResult(new ResponseModel(result.Errors));
            }
            
            var updatedUser = Mapper.Map<ApplicationUserModel>(await GetApplicationUserById(applicationUser.Id, true));

            return new OkObjectResult(new ResponseModel{
                Object = updatedUser
            });
        }

        public async Task<ApplicationUser> ChangeProfilePicture(Guid imageBlobId, ApplicationUser applicationUser)
        {
            var imageBlob = _context.ImageBlobs
                                .Where(x => x.BlobId == imageBlobId)
                                .FirstOrDefault();

            if(imageBlob == null)
                throw new ErrorModelException(ErrorCodes.IncorrectValue, nameof(ApplicationUserModel.ProfilePictureId));

            applicationUser.ProfilePicture = imageBlob;

            await _userManager.UpdateAsync(applicationUser);

            return applicationUser;
        }
        
        public async Task<ApplicationUser> RemoveProfilePicture(ApplicationUser applicationUser)
        {
            applicationUser.ProfilePicture = null;
            applicationUser.ProfilePictureId = null;

            await _userManager.UpdateAsync(applicationUser);

            return applicationUser;
        }

        public async Task<ApplicationUser> UpdateUserRating(Guid applicationUserId)
        {
            var applicationUser = await _context.ApplicationUsers.Where(x => x.Id.Equals(applicationUserId)).FirstOrDefaultAsync();

            if(applicationUser == null)
                throw new ErrorModelException(ErrorCodes.EntityNotFound, nameof(ApplicationUser));

            var averageRating = await _context.Feedbacks.Where(x => x.TeacherId.Equals(applicationUserId)).AverageAsync(y => y.Rating);
            
            applicationUser.AverageRating = averageRating;
            _context.Update(applicationUser);
            await _context.SaveChangesAsync();

            return applicationUser;
        }

        public async Task<IActionResult> ConfirmEmail(ConfirmEmailModel confirmEmailModel)
        {
            var user = await _userManager.FindByEmailAsync(confirmEmailModel.Email);
            if (user == null)
            {
                throw new FormErrorModelException(nameof(confirmEmailModel.Email), ErrorCodes.EmailInvalid, nameof(ApplicationUser));
            }

            var correctPassword = await _userManager.CheckPasswordAsync(user, confirmEmailModel.Password);
            if(!correctPassword)
            {
                var formError = new FormErrorModel(nameof(confirmEmailModel.Password), ErrorCodes.PasswordIncorrect);
                return new BadRequestObjectResult(new ResponseModel(formError));
            }

            var result = await _userManager.ConfirmEmailAsync(user, confirmEmailModel.Code);
            if(!result.Succeeded)
            {
                var formError = new FormErrorModel(nameof(confirmEmailModel.Code), ErrorCodes.ConfirmationCodeIncorrect);
                return new BadRequestObjectResult(new ResponseModel(formError));
            }

            return new OkObjectResult(new ResponseModel(){ Object = user });
        }

        public async Task<ApplicationUser> GetApplicationUserById(Guid userId, bool includeDetails = false)
        {
            IQueryable<ApplicationUser> userQuery = _context.ApplicationUsers
                                                            .Where(x => x.Id.Equals(userId));
                    
            if(includeDetails)
                userQuery = userQuery.Include(x => x.ApplicationUserLanguages)
                                        .ThenInclude(x => x.Language)
                                        .Include(x => x.ApplicationUserInterests)
                                        .ThenInclude(x => x.Fos)
                                        .Include(x => x.MotherTongue)
                                        .Include(x => x.Country)
                                        .Include(x => x.State);

            var QueryResult = await userQuery.GroupJoin(_context.AccountTransactions.OrderByDescending(o => o.Timestamp),
                                        x => x.Id,
                                        y => y.ApplicationUserId,
                                        (x,y) => new {User = x, Trans = y})
                                        .FirstOrDefaultAsync();
            

            if(QueryResult == null)
                throw new ErrorModelException(ErrorCodes.EntityNotFound, nameof(ApplicationUser));

            QueryResult.User.MinutesBalance = QueryResult.Trans.FirstOrDefault()?.FinalMinutesBalance;
            QueryResult.User.PointsBalance = QueryResult.Trans.FirstOrDefault()?.FinalPointsBalance;

            return QueryResult.User;   
        }

        public async Task<List<ApplicationUser>> Search(string searchString, Guid applicationUserId)
        {
            if(String.IsNullOrWhiteSpace(searchString))
                return null;
                
            searchString = searchString.ToLower();

            var blockedFriendship = await _context.Friendships.Where(x => x.ApplicationUserBiggerId.Equals(applicationUserId) || x.ApplicationUserSmallerId.Equals(applicationUserId))
                                                         .Where(x => x.Status == FriendshipStatus.Blocked)
                                                         .Select(x => (x.ApplicationUserSmallerId.Equals(applicationUserId)?x.ApplicationUserSmallerId:x.ApplicationUserBiggerId))
                                                         .ToListAsync();

            var result = await _context.ApplicationUsers.Where(x => $"{x.Name} {x.Surname}".ToLower().Contains(searchString)).Take(6).ToListAsync();
            
            foreach (var user in result)
                if(blockedFriendship.Contains(user.Id))
                    result.Remove(user);
            
            return result;
        }
    }
}