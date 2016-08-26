using System.Threading.Tasks;
using Knowlead.DTO;

namespace Knowlead.BLL.Interfaces
{
    public interface IAccountRepository
    {
        Task<ResponseModel> RegisterApplicationUserAsync(ApplicationUserModel applicationUserModel);
    }
}