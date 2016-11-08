using System;
using Knowlead.DTO.ResponseModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.JsonPatch.Exceptions;

namespace Knowlead.WebApi.Attributes
{
    public class HandleExceptionsAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            Exception ex = filterContext.Exception;
            int statusCode = 400;
            filterContext.ExceptionHandled = true;

            var exType = filterContext.Exception.GetType();
            var responseModel = new ResponseModel();
            
            if(exType == typeof(JsonPatchException))
            {
                responseModel.AddError(new ErrorModel(ex.Message));
            }
            //If no specific exception occured
            else
            {
                responseModel.AddError(new ErrorModel(ex.Message));
                responseModel.AddError(new ErrorModel(ex?.InnerException.Message));
                statusCode = 500;
            }
            
            filterContext.Result = new BadRequestObjectResult(responseModel)
            {
                StatusCode = statusCode
            };
        }
    }
}