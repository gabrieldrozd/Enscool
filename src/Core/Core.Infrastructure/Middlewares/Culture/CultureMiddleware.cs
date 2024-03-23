using System.Globalization;
using Microsoft.AspNetCore.Http;

namespace Core.Infrastructure.Middlewares.Culture;

internal sealed class CultureMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var cultureString = context.Request.Headers["X-Language"].ToString();
        CultureInfo.CurrentUICulture = UserCulture.Create(cultureString).Value;
        await next(context);
    }
}