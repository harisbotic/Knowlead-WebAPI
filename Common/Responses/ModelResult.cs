using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Knowlead.Common.Responses
{
    public class ModelResult : ObjectResult
    {
        public int ResponseCode { get; set; }

        public ModelResult(object model, int responseCode = 200)
            :base(model)
        {
            ResponseCode = responseCode;
        }

        public override Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.StatusCode = ResponseCode;
            return base.ExecuteResultAsync(context);
        }
    }
}