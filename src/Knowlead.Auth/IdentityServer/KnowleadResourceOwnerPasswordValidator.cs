using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Knowlead.Common;
using Knowlead.DAL;
using Knowlead.DomainModel.UserModels;
using Microsoft.AspNetCore.Identity;

namespace Knowlead.Auth.IdentityServer
{
    public class KnowleadResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public KnowleadResourceOwnerPasswordValidator(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// Validates the resource owner password credential
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await _userManager.FindByEmailAsync(context.UserName);
            if (user == null)
            {
                context.Result = new GrantValidationResult(
                    TokenRequestErrors.InvalidGrant,
                    Constants.ErrorCodes.LoginCredentialsIncorrect);
                    
                return;
            }

            // Ensure the password is valid.
            if (!await _userManager.CheckPasswordAsync(user, context.Password))
            {
                if (_userManager.SupportsUserLockout)
                {
                    await _userManager.AccessFailedAsync(user);
                }

                context.Result = new GrantValidationResult(
                    TokenRequestErrors.InvalidGrant,
                    Constants.ErrorCodes.LoginCredentialsIncorrect);
                
                return;
            }

            if (!user.EmailConfirmed)
            {
                context.Result = new GrantValidationResult(
                    TokenRequestErrors.InvalidGrant,
                    Constants.ErrorCodes.EmailNotVerified);

                return;
            }

            //If there are no errors
            if (_userManager.SupportsUserLockout)
            {
                await _userManager.ResetAccessFailedCountAsync(user);
            }

            context.Result = new GrantValidationResult(
                subject: user.Id.ToString(),
                authenticationMethod: OidcConstants.AuthenticationMethods.Password);
        }
    }
}