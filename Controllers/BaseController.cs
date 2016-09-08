
using System.Threading.Tasks;
using Knowlead.BLL.Interfaces;
using Knowlead.DomainModel.UserModels;
using Microsoft.AspNetCore.Mvc;

namespace Knowlead.Controllers
{
    public class BaseController : Controller
    {
        protected IAccountRepository _accountRepository;
        protected BaseController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        protected async Task<ApplicationUser> GetCurrentUserAsync()
        {
            return await _accountRepository.GetUserByPrincipal(User);
        }
    }
}