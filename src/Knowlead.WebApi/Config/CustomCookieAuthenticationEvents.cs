using System.Text;
using System.Threading.Tasks;
using Knowlead.Common.Exceptions;
using Knowlead.DTO.ResponseModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using static Knowlead.Common.Constants;

public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
{
    public CustomCookieAuthenticationEvents()
    {
    }

    public override Task RedirectToLogin(CookieRedirectContext context)
    {
        WriteResponse(new ErrorModel(ErrorCodes.NotLoggedIn), context);

        return Task.CompletedTask;
    }

    public override Task RedirectToAccessDenied(CookieRedirectContext context)
    {
        WriteResponse(new ErrorModel(ErrorCodes.AuthorityError), context);

        return Task.CompletedTask;
    }

    private void WriteResponse(ErrorModel errorModel, CookieRedirectContext context)
    {
        var serializerSettings = context
                .HttpContext
                .RequestServices
                .GetRequiredService<IOptions<MvcJsonOptions>>()
                .Value
                .SerializerSettings;

            context.Response.ContentType = "application/json";

            using (var writer = new HttpResponseStreamWriter(context.Response.Body, Encoding.UTF8))
            {
                using (var jsonWriter = new JsonTextWriter(writer))
                {
                    jsonWriter.CloseOutput = false;
                    var jsonSerializer = JsonSerializer.Create(serializerSettings);
                    jsonSerializer.Serialize(jsonWriter, new ResponseModel(errorModel));
                }
            }
    }
}