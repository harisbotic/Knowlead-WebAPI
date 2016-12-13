/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/openiddict/openiddict-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Extensions;
using AspNet.Security.OpenIdConnect.Primitives;
using AspNet.Security.OpenIdConnect.Server;
using Knowlead.Common;
using Knowlead.DomainModel.UserModels;
using Knowlead.DTO.ResponseModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Core;

namespace Mvc.Server
{
    public class AuthorizationController : Controller {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthorizationController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager) {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("~/connect/token")]
        [Produces("application/json")]
        public async Task<IActionResult> Exchange(OpenIdConnectRequest request) 
        {
            if (request.IsPasswordGrantType())
            {
                var user = await _userManager.FindByEmailAsync(request.Username);
                if (user == null) {
                    return BadRequest(new ResponseModel(new ErrorModel {
                        Value = Constants.ErrorCodes.LoginCredentialsIncorrect
                    }));
                }
        
              // Ensure the user is allowed to sign in.
                if (!await _signInManager.CanSignInAsync(user)) {
                    return BadRequest(new OpenIdConnectResponse {
                        Error = OpenIdConnectConstants.Errors.InvalidGrant,
                        ErrorDescription = "The specified user is not allowed to sign in."
                    });
                }

                // Ensure the password is valid.
                if (!await _userManager.CheckPasswordAsync(user, request.Password)) {
                    if (_userManager.SupportsUserLockout) {
                        await _userManager.AccessFailedAsync(user);
                    }
        
                    return BadRequest(new ResponseModel(new ErrorModel {
                        Value = Constants.ErrorCodes.LoginCredentialsIncorrect
                    }));
                }
                
                if (!user.EmailConfirmed)
                {
                    return BadRequest(new ResponseModel(new ErrorModel {
                        Value = Constants.ErrorCodes.EmailNotVerified
                    }));
                }

                if (_userManager.SupportsUserLockout) {
                    await _userManager.ResetAccessFailedCountAsync(user);
                }
        
                // Create a new authentication ticket.
                var ticket = await CreateTicketAsync(request, user);

                return SignIn(ticket.Principal, ticket.Properties, ticket.AuthenticationScheme);
            }

            return BadRequest(new ResponseModel(new ErrorModel {
                Value = "The specified grant type is not supported."
            }));
        }

        [Authorize, HttpPost("~/connect/authorize/deny")]
        public IActionResult Deny() 
        {
            // Notify OpenIddict that the authorization grant has been denied by the resource owner
            // to redirect the user agent to the client application using the appropriate response_mode.
            return Forbid(OpenIdConnectServerDefaults.AuthenticationScheme);
        }

        [HttpPost("~/connect/logout")]
        public async Task<IActionResult> Logout(CancellationToken cancellationToken)
        {
            // Ask ASP.NET Core Identity to delete the local and external cookies created
            // when the user agent is redirected from the external identity provider
            // after a successful authentication flow (e.g Google or Facebook).
            await _signInManager.SignOutAsync();

            // Returning a SignOutResult will ask OpenIddict to redirect the user agent
            // to the post_logout_redirect_uri specified by the client application.
            return SignOut(OpenIdConnectServerDefaults.AuthenticationScheme);
        }

         private async Task<AuthenticationTicket> CreateTicketAsync(OpenIdConnectRequest request, ApplicationUser user) {
            // Create a new ClaimsPrincipal containing the claims that
            // will be used to create an id_token, a token or a code.
            var principal = await _signInManager.CreateUserPrincipalAsync(user);

            // Note: by default, claims are NOT automatically included in the access and identity tokens.
            // To allow OpenIddict to serialize them, you must attach them a destination, that specifies
            // whether they should be included in access tokens, in identity tokens or in both.

            foreach (var claim in principal.Claims) {
                // In this sample, every claim is serialized in both the access and the identity tokens.
                // In a real world application, you'd probably want to exclude confidential claims
                // or apply a claims policy based on the scopes requested by the client application.
                claim.SetDestinations(OpenIdConnectConstants.Destinations.AccessToken,
                                      OpenIdConnectConstants.Destinations.IdentityToken);
            }

            // Create a new authentication ticket holding the user identity.
            var ticket = new AuthenticationTicket(
                principal, new AuthenticationProperties(),
                OpenIdConnectServerDefaults.AuthenticationScheme);

            // Set the list of scopes granted to the client application.
            // Note: the offline_access scope must be granted
            // to allow OpenIddict to return a refresh token.
            ticket.SetScopes(new[] {
                OpenIdConnectConstants.Scopes.OpenId,
                OpenIdConnectConstants.Scopes.Email,
                OpenIdConnectConstants.Scopes.Profile,
                OpenIdConnectConstants.Scopes.OfflineAccess,
                OpenIddictConstants.Scopes.Roles
            }.Intersect(request.GetScopes()));

            //ADDED MANUALLY
            ticket.SetResources(request.GetResources());
            ticket.SetScopes(request.GetScopes());

            return ticket;
        }
    }
}