using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Knowlead.DomainModel.UserModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Options;

public class AppClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole<Guid>>
{
    public AppClaimsPrincipalFactory(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole<Guid>> roleManager,
        IOptions<IdentityOptions> optionsAccessor) : base(userManager, roleManager, optionsAccessor)
    {
    }

    public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
    {
        var principal = await base.CreateAsync(user);

        var claims = new List<Claim>();

        if(user.Name != null)
            claims.Add(new Claim(ClaimTypes.GivenName, user.Name));

        if(user.Surname != null)
            claims.Add(new Claim(ClaimTypes.Name, user.Surname));

        if(user.PhoneNumber != null)
            claims.Add(new Claim(ClaimTypes.Name, user.PhoneNumber));

        ((ClaimsIdentity)principal.Identity).AddClaims(claims);

        return principal;
    }
}