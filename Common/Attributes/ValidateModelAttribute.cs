using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Knowlead.DTO;
using Knowlead.DTO.ApplicationUserModels;
using Knowlead.DTO.Mappers;
using Knowlead.Utils;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using static Knowlead.Common.Constants;

namespace Knowloead.Common.Attributes
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        private Object _argument;
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.Values.Count != 1)
            {
                throw new InvalidOperationException("Number of arguments in action must be exactly 1 for ValidateModel attribute");
            }

            _argument = context.ActionArguments.Values.First();

            if (_argument == null)
            {
                context.Result = new BadRequestObjectResult(new ResponseModel(false,
                    new ErrorModel("Model is empty", ErrorCodes.ValidationError)
                ));
            }

            else if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(new ResponseModel(false,
                    context.ModelState.AsDictionary().MapToErrorDictionary((int)ErrorCodes.ValidationError)
                ));
            }

            else if (context.HttpContext.Request.Method.ToUpper() == "PATCH")
            {
                if (_argument.GetType().Namespace != typeof(JsonPatchDocument).Namespace)
                {
                    throw new InvalidOperationException("Argument must be of type JsonPatchDocument");
                }

                dynamic jsonPatchDocument = Convert.ChangeType(_argument, _argument.GetType());

                var dto = _argument.GetType().GetTypeInfo().GenericTypeArguments[0];
                dynamic dtoInstance = Activator.CreateInstance(dto) as dynamic;

                try
                {
                    jsonPatchDocument.ApplyTo(dtoInstance);
                }
                catch (Exception ex)
                {
                    context.Result = new BadRequestObjectResult(new ResponseModel(false,
                    new ErrorModel(ex.Message, ErrorCodes.ValidationError)));
                }
            }
        }
    }
}