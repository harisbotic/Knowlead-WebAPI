using Microsoft.AspNetCore.Mvc.Filters;
using Knowlead.DTO.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using Knowlead.DomainModel.UserModels;
using Knowlead.BLL.Interfaces;
using System.Threading.Tasks;
using Knowlead.Common;

namespace Knowlead.WebApi.Attributes
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
                    var error = new ErrorModel(Constants.ErrorCodes.NotLoggedIn);
                    context.Result = new BadRequestObjectResult(new ResponseModel(error));

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
                        var error = new ErrorModel(Constants.ErrorCodes.EmailNotVerified);
                        context.Result = new BadRequestObjectResult(new ResponseModel(error));
                        return;
                    }
                }
            }
        }
    }
}