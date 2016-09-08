using System.Security.Claims;
using System.Threading.Tasks;
using Knowlead.DomainModel.UserModels;
using Knowlead.DTO;
using Knowlead.DTO.ApplicationUserModels;
using Microsoft.AspNetCore.JsonPatch;

namespace Knowlead.BLL.Interfaces
{
    public interface IAccountRepository
    {
        Task<ResponseModel> RegisterApplicationUserAsync(RegisterUserModel applicationUserModel);
        Task<ApplicationUser> GetUserByPrincipal(ClaimsPrincipal principal);
        Task<ResponseModel> UpdateUserDetails(ApplicationUser applicationUser, JsonPatchDocument<UserDetailsModel> userDetailsPatch);
        Task<ResponseModel> ConfirmEmail(ConfirmEmailModel confirmEmailModel);
        Task<ResponseModel> SaveChangesAsync();
    }
}