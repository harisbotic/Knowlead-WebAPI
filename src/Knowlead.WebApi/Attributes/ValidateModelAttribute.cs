using System;
using System.Linq;
using System.Reflection;
using Knowlead.DTO.ResponseModels;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Knowlead.Common.Attributes
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        private Object _argument;
        private ResponseModel responseModel;
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.Values.Count != 1)
            {
                throw new InvalidOperationException("Number of arguments in action must be exactly 1 for ValidateModel attribute");
            }

            _argument = context.ActionArguments.Values.First();
            responseModel = new ResponseModel();
            if (_argument == null)
            {
                var error = new ErrorModel(Constants.ErrorCodes.ModelEmpty);
                context.Result = new BadRequestObjectResult(new ResponseModel(error));
            }

            else if (!context.ModelState.IsValid)
            {
                responseModel.AddErrors(context.ModelState);
                context.Result = new BadRequestObjectResult(responseModel);
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
                    var error = new ErrorModel(ex.Message);
                    context.Result = new BadRequestObjectResult(new ResponseModel(error));
                }
            }
        }
    }
}