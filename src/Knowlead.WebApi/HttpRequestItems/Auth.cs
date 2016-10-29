using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Knowlead.DAL;
using Knowlead.DomainModel.UserModels;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Knowlead.Common.HttpRequestItems
{
    public class Auth
    {
        private const string UserKey = "Knowlead.Auth.UserKey";
    
        private readonly IHttpContextAccessor _accessor;
        private readonly ApplicationDbContext _context;

        public Auth(IHttpContextAccessor accessor,
                    ApplicationDbContext context)
        {
            _accessor = accessor;
            _context = context;
        }
        public async Task<ApplicationUser> GetUser(bool includeDetails = false)
        {
            if (!_accessor.HttpContext.User.Identity.IsAuthenticated)
                    return null;

                var user = _accessor.HttpContext.Items[UserKey] as ApplicationUser;
                if (user == null)
                {

                    var userId = _accessor.HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                                                                  .Select(c => c.Value).FirstOrDefault();

                    IQueryable<ApplicationUser> userQuery = _context.ApplicationUsers;
                    
                    if(includeDetails)
                        userQuery = userQuery.Include(x => x.ApplicationUserLanguages)
                                                .ThenInclude(x => x.Language)
                                             .Include(x => x.ApplicationUserInterests)
                                                .ThenInclude(x => x.Fos)
                                             .Include(x => x.MotherTongue)
                                             .Include(x => x.Country)
                                             .Include(x => x.State);

                    userQuery.Where(x => x.Id == new Guid(userId));

                    user = await userQuery.FirstOrDefaultAsync();

                    if (user == null)
                        return null;

                    _accessor.HttpContext.Items[UserKey] = user;
                }
                return user;

        }
    }
}