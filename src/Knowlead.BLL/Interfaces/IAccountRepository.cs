using System.Security.Claims;
using System.Threading.Tasks;
using Knowlead.DomainModel.UserModels;
using Knowlead.DTO.UserModels;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Knowlead.BLL.Interfaces
{
    public interface IAccountRepository
    {
        Task<IActionResult> RegisterApplicationUserAsync(RegisterUserModel registerUserModel);
        Task<ApplicationUser> GetUserByPrincipal(ClaimsPrincipal principal);
        Task<IActionResult> UpdateUserDetails(ApplicationUser applicationUser, JsonPatchDocument<ApplicationUserModel> applicationUserPatch);
        Task<IActionResult> ConfirmEmail(ConfirmEmailModel confirmEmailModel);
        Task<IActionResult> SaveChangesAsync();
    }
}