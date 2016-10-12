using System.Threading.Tasks;
using Knowlead.DomainModel.UserModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Knowlead.Common.HttpRequestItems
{
    public class Auth
    {
        private const string UserKey = "Knowlead.Auth.UserKey";
    
        private readonly IHttpContextAccessor _accessor;

        private readonly UserManager<ApplicationUser> _userManager;

        public Auth(IHttpContextAccessor accessor, UserManager<ApplicationUser> userManager)
        {
            _accessor = accessor;
            _userManager = userManager;
        }
        public async Task<ApplicationUser> GetUser()
        {
            if (!_accessor.HttpContext.User.Identity.IsAuthenticated)
                    return null;

                var user = _accessor.HttpContext.Items[UserKey] as ApplicationUser;
                if (user == null)
                {
                    user = await _userManager.GetUserAsync(_accessor.HttpContext.User);

                    if (user == null)
                        return null;

                    _accessor.HttpContext.Items[UserKey] = user;
                }
                return user;

        }
    }
}