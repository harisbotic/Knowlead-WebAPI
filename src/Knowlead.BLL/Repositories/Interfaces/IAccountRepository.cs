using System;
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
        Task<IActionResult> ConfirmEmail(ConfirmEmailModel confirmEmailModel);
    }
}