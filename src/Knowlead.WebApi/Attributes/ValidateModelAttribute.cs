using System;
using System.Linq;
using Knowlead.Common.Exceptions;
using Knowlead.DTO.ResponseModels;
using Microsoft.AspNetCore.Http.Internal;
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
            if (context.ActionArguments.Values.Count == 1)
            {
                _argument = context.ActionArguments.Values.First();
                responseModel = new ResponseModel();
            }
            else {
                throw new InvalidOperationException("Number of arguments in action must be exactly 1 for ValidateModel attribute");
            }
 
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
 
            else if (_argument.GetType() == typeof(FormFile))
            {
                var file = _argument as FormFile;
                ErrorModel error = null;
               
                if (file == null) error = new ErrorModel("File is null");
                if (file.Length == 0) error = new ErrorModel("File is empty");
 
                if(error != null)
                    context.Result = new BadRequestObjectResult(new ResponseModel(error));
                   
            }
 
            // else if (context.HttpContext.Request.Method.ToUpper() == "PATCH")
            // {
            //     if (_argument.GetType().Namespace != typeof(JsonPatchDocument).Namespace)
            //     {
            //         throw new InvalidOperationException("Argument must be of type JsonPatchDocument");
            //     }
 
            //     dynamic jsonPatchDocument = Convert.ChangeType(_argument, _argument.GetType());
 
            //     var dto = _argument.GetType().GetTypeInfo().GenericTypeArguments[0];
            //     dynamic dtoInstance = Activator.CreateInstance(dto) as dynamic;
 
            //     try
            //     {
            //         jsonPatchDocument.ApplyTo(dtoInstance);
            //     }
            //     catch (Exception ex)
            //     {
            //         var error = new ErrorModel(ex.Message);
            //         //context.Result = new BadRequestObjectResult(new ResponseModel(error));
            //     }
            // }
        }
    }
}