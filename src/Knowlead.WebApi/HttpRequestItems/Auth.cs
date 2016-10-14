using System.Linq;
using System.Threading.Tasks;
using Knowlead.DAL;
using Knowlead.DomainModel.LookupModels.Core;
using Knowlead.DomainModel.UserModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Knowlead.Common.HttpRequestItems
{
    public class Auth
    {
        private const string UserKey = "Knowlead.Auth.UserKey";
    
        private readonly IHttpContextAccessor _accessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public Auth(IHttpContextAccessor accessor,
                    UserManager<ApplicationUser> userManager,
                    ApplicationDbContext context)
        {
            _accessor = accessor;
            _userManager = userManager;
            _context = context;
        }
        public async Task<ApplicationUser> GetUser(bool languages = false, bool geo = false)
        {
            if (!_accessor.HttpContext.User.Identity.IsAuthenticated)
                    return null;

                var user = _accessor.HttpContext.Items[UserKey] as ApplicationUser;
                if (user == null)
                {
                    IQueryable<ApplicationUser> userQuery = _context.ApplicationUsers;
                    
                    if(languages)
                        userQuery = userQuery.Include(x => x.ApplicationUserLanguages);
                    if(languages)
                        userQuery = userQuery.Include(x => x.MotherTongue);
                    if(geo)
                        userQuery = userQuery.Include(x => x.Country);
                    if(geo)
                        userQuery = userQuery.Include(x => x.State);

                    userQuery.Where(x => x.UserName == _accessor.HttpContext.User.Identity.Name);

                    user = await userQuery.SingleAsync();

                    if (user == null)
                        return null;

                    _accessor.HttpContext.Items[UserKey] = user;
                }
                return user;

        }
    }
}