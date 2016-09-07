using System.Security.Claims;
using System.Threading.Tasks;
using Knowlead.DomainModel.UserModels;
using Knowlead.DTO;
using Knowlead.DTO.ApplicationUserModels;

namespace Knowlead.BLL.Interfaces
{
    public interface IAccountRepository
    {
        Task<ResponseModel> RegisterApplicationUserAsync(RegisterUserModel applicationUserModel);
        Task<ApplicationUser> GetUserByPrincipal(ClaimsPrincipal principal);
        Task<ResponseModel> UpdateUserDetails(ApplicationUser applicationUser, UserDetailsModel userDetailsModel);
        Task<ResponseModel> ConfirmEmail(ConfirmEmailModel confirmEmailModel);
        Task<ResponseModel> SaveChangesAsync();
    }
}