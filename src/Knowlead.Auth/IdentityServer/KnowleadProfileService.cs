using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Knowlead.Common;
using Knowlead.DAL;

namespace Knowlead.Auth.IdentityServer
{
    public class KnowleadProfileService : IProfileService
    {
        ApplicationDbContext _context;
        public KnowleadProfileService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// This method is called whenever claims about the user are requested (e.g. during token creation or via the userinfo endpoint)
        /// </summary>
        public virtual Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var subject = context.Subject.Claims.GetUserIdFromClaims();
            var user = _context.ApplicationUsers.Where(x => x.Id.Equals(subject)).FirstOrDefault();
            
            if(user == null)
                return Task.FromResult(0);

            // if (context.RequestedClaimTypes.Any()) {

            var claimList = new List<Claim>();

            if(user.Name != null)
                claimList.Add(new Claim(ClaimTypes.GivenName, user.Name));

            if(user.Surname != null)
                claimList.Add(new Claim(ClaimTypes.Name, user.Surname));

            if(user.PhoneNumber != null)
                claimList.Add(new Claim(ClaimTypes.Name, user.PhoneNumber));

            // context.AddFilteredClaims(claimList);
            context.IssuedClaims = claimList;

            return Task.FromResult(0);
        }

        /// <summary>
        /// This method gets called whenever identity server needs to determine if the user is valid or active (e.g. if the user's account has been deactivated since they logged in).
        /// (e.g. during token issuance or validation).
        /// </summary>
        public virtual Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;

            return Task.FromResult(0);
        }
    }
}