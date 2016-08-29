using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Knowlead.Common.Responses;
using Knowlead.DTO;
using static Knowlead.Common.Constants;
using Microsoft.AspNetCore.Mvc;
using Knowlead.BLL;
using Knowlead.DomainModel.UserModels;
using Knowlead.BLL.Interfaces;

namespace Knowlead.Common.Attributes
{
    class ReallyAuthorizeAttribute : TypeFilterAttribute
    {
        public ReallyAuthorizeAttribute() : base(typeof(ReallyAuthorizedAttributeImpl))
        {
        }

        private class ReallyAuthorizedAttributeImpl : Attribute, IAuthorizationFilter
        {
            IAccountRepository _accountRepository;
            public ReallyAuthorizedAttributeImpl(IAccountRepository repository)
            {
                _accountRepository = repository;
            }
            public bool CheckEmail { get; set; } = false;
            public async void OnAuthorization(AuthorizationFilterContext context)
            {
                if (!context.HttpContext.User.Identity.IsAuthenticated)
                {
                    context.Result = new ModelResult(new ErrorModel("Not logged in", ErrorCodes.NotLoggedIn), 403);
                    return;
                }
                if (CheckEmail)
                {
                    ApplicationUser user = await _accountRepository.GetUserByPrincipal(context.HttpContext.User);
                    if (!user.EmailConfirmed)
                    {
                        context.Result = new ModelResult(new ErrorModel("Email not verified", ErrorCodes.EmailNotConfirmed), 403);
                    }
                }
            }
        }
    }
}