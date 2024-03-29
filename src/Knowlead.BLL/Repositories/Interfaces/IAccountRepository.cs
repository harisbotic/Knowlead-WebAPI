using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Knowlead.DomainModel.UserModels;
using Knowlead.DTO.UserModels;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Knowlead.BLL.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<IActionResult> RegisterApplicationUserAsync(RegisterUserModel registerUserModel);
        Task<ApplicationUser> GetApplicationUserById(Guid userId, bool includeDetails = false);
        Task<IActionResult> UpdateUserDetails(ApplicationUser applicationUser, JsonPatchDocument<ApplicationUserModel> applicationUserPatch);
        Task<ApplicationUser> ChangeProfilePicture(Guid imageBlobId, ApplicationUser applicationUser);
        Task<ApplicationUser> RemoveProfilePicture(ApplicationUser applicationUser);
        Task<ApplicationUser> UpdateUserRating(Guid applicationUserId);
        Task<IActionResult> ConfirmEmail(ConfirmEmailModel confirmEmailModel);
        Task<bool> GeneratePasswordResetTokenAsync (string email);
        Task<bool> ResetPasswordAsync (PasswordResetModel resetPasswordModel);
        Task<List<ApplicationUserReferral>> GetReferrals(Guid applicationUser, bool registred = true);
        Task<int> GetReferralsCount(Guid applicationUser, bool registred = true);
        Task<List<ApplicationUser>> Search(string searchString, Guid applicationUserId);
        
    }
}