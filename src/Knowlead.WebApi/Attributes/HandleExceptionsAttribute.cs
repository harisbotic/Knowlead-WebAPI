using System;
using Knowlead.DTO.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.JsonPatch.Exceptions;
using Knowlead.Common.Exceptions;

namespace Knowlead.WebApi.Attributes
{
    public class HandleExceptionsAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;
            int statusCode = 400;

            var exType = ex.GetType();
            var responseModel = new ResponseModel();
            
            if(exType == typeof(JsonPatchException))
            {
                responseModel.AddError(new ErrorModel(ex.Message));
            }
            else if(exType == typeof(ErrorModelException))
            {
                var eme = ex as ErrorModelException;
                responseModel.AddError(eme.Error);
            }
            else if(exType == typeof(FormErrorModelException))
            {
                var feme = ex as FormErrorModelException;
                responseModel.AddError(feme.FormError);
            }

            //If no specific exception occured
            else
            {
                responseModel.AddError(new ErrorModel(ex.Message));
                if(ex?.InnerException != null)
                    responseModel.AddError(new ErrorModel(ex.InnerException.Message));
                statusCode = 500;
            }
            
            filterContext.Result = new BadRequestObjectResult(responseModel)
            {
                StatusCode = statusCode
            };
        }
    }
}