using Common.Extensions;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Net;

namespace EndPoint.Api.Extensions.Middleware;

public static class ConfiguredExceptionHandlerMiddleware
{
    public static void UseConfiguredExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseExceptionHandler(errorApp =>
            errorApp.Run(async context =>
            {
                const HttpStatusCode responseStatus = HttpStatusCode.InternalServerError;
                context.Response.StatusCode = (int)responseStatus;
                context.Response.ContentType = "application/json";

                var error = context.Features.Get<IExceptionHandlerFeature>()?.Error;

                if (error == null)
                    return;

                string json;

                if (error is AppException er)
                {
                    context.Response.StatusCode = (int)er.Status;

                    json = JsonConvert.SerializeObject(new
                    {
                        IsSuccess = false,
                        ResponseType = er.Status,
                        ResponseDesc = error.Message
                    });
                }
                else if (env.IsProduction())
                    json = JsonConvert.SerializeObject(new
                    {
                        context.Response.StatusCode,
                        Title = responseStatus.ToString()
                    });
                else
                    json = JsonConvert.SerializeObject(new
                    {
                        context.Response.StatusCode,
                        Title = responseStatus.ToString(),
                        error.Message,
                        error.StackTrace
                    });

                await context.Response.WriteAsync(json);
            })
        );
    }
}