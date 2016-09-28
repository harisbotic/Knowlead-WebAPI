using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Knowlead.DTO.ResponseModels;

namespace Knowlead.WebApi.Attributes
{
    class ResponseModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            ObjectResult result = context.Result as ObjectResult;

            if (result != null)
            {
                ResponseModel val = result.Value as ResponseModel;
                if (val != null)
                {
                    // If first number of status code is 2, which means OK response
                    if (!val.Success && context.HttpContext.Response.StatusCode / 100 == 2)
                    {
                        context.HttpContext.Response.StatusCode = 400;
                    }
                }
            }
        }
    }    
}