using Microsoft.AspNetCore.Mvc.Filters;
using Knowlead.Common.Responses;
using Knowlead.DTO;
using static Knowlead.Common.Constants;
using Microsoft.AspNetCore.Mvc;
using Knowlead.DomainModel.UserModels;
using Knowlead.BLL.Interfaces;
using System.Threading.Tasks;

namespace Knowlead.Common.Attributes
{
    class ReallyAuthorizeAttribute : TypeFilterAttribute
    {
        public ReallyAuthorizeAttribute(bool verifyEmail = true) :
            base((verifyEmail) ? typeof(ReallyAuthorizedEmailAttributeImpl) : typeof(ReallyAuthorizedAttributeImpl))
        {
        }

        private class ReallyAuthorizedAttributeImpl : IAsyncAuthorizationFilter
        {
            public ReallyAuthorizedAttributeImpl()
            {
            }

            public virtual Task OnAuthorizationAsync(AuthorizationFilterContext context)
            {
                if (!context.HttpContext.User.Identity.IsAuthenticated)
                {
                    context.Result = new ModelResult(new ErrorModel("Not logged in", ErrorCodes.NotLoggedIn), 403);
                }
                return Task.CompletedTask;
            }
        }

        private class ReallyAuthorizedEmailAttributeImpl : ReallyAuthorizedAttributeImpl
        {
            IAccountRepository _accountRepository;
            public ReallyAuthorizedEmailAttributeImpl(IAccountRepository repository)
            {
                _accountRepository = repository;
            }

            public async override Task OnAuthorizationAsync(AuthorizationFilterContext context)
            {
                await base.OnAuthorizationAsync(context);
                if (context.Result == null)
                {
                    ApplicationUser user = await _accountRepository.GetUserByPrincipal(context.HttpContext.User);
                    if (!user.EmailConfirmed)
                    {
                        context.Result = new ModelResult(new ErrorModel("Email not verified", ErrorCodes.EmailNotConfirmed), 403);
                        return;
                    }
                }
            }
        }
    }
}