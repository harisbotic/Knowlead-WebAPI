using Microsoft.AspNetCore.Mvc.Filters;
using static Knowlead.Common.Constants;
using Knowlead.BLL.Exceptions;

namespace Knowlead.WebApi.Attributes
{
    public class ReallyAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                throw new ErrorModelException(ErrorCodes.NotLoggedIn);
            }
        }
    }
}