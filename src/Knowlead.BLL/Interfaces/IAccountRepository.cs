using System.Security.Claims;
using System.Threading.Tasks;
using Knowlead.DomainModel.UserModels;
using Knowlead.DTO.ResponseModels;
using Knowlead.DTO.UserModels;
using Microsoft.AspNetCore.JsonPatch;

namespace Knowlead.BLL.Interfaces
{
    public interface IAccountRepository
    {
        Task<ResponseModel> RegisterApplicationUserAsync(RegisterUserModel registerUserModel);
        Task<ApplicationUser> GetUserByPrincipal(ClaimsPrincipal principal);
        Task<ResponseModel> UpdateUserDetails(ApplicationUser applicationUser, JsonPatchDocument<ApplicationUserModel> applicationUserPatch);
        Task<ResponseModel> ConfirmEmail(ConfirmEmailModel confirmEmailModel);
        Task<ResponseModel> SaveChangesAsync();
    }
}