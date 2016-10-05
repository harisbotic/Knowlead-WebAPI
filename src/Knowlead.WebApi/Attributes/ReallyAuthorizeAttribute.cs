using Microsoft.AspNetCore.Mvc.Filters;
using Knowlead.DTO.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using Knowlead.Common;

namespace Knowlead.WebApi.Attributes
{
    public class ReallyAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                var error = new ErrorModel(Constants.ErrorCodes.NotLoggedIn);
                context.Result = new BadRequestObjectResult(new ResponseModel(error));
            }
        }
    }
}