using System;
using System.Linq;
using Knowlead.DTO;
using Knowlead.DTO.Mappers;
using Knowlead.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using static Knowlead.Common.Constants;

namespace Knowloead.Common.Attributes
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.Values.Count != 1)
            {
                throw new InvalidOperationException("Number of arguments in action must be exactly 1 for ValidateModel attribute");
            }
            if (context.ActionArguments.Values.First() == null) 
            {
                context.Result = new BadRequestObjectResult(new ResponseModel(false,
                    new ErrorModel("Model is empty", ErrorCodes.ValidationError)
                ));
            }
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(new ResponseModel(false,
                    context.ModelState.AsDictionary().MapToErrorDictionary((int)ErrorCodes.ValidationError)
                ));
            }
        }
    }
}