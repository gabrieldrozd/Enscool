using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Core.Infrastructure.Middlewares.Http;

internal sealed class HttpRequestMiddleware : IMiddleware
{
    private readonly ILogger<HttpRequestMiddleware> _logger;
    private readonly Stopwatch _timer;

    public HttpRequestMiddleware(ILogger<HttpRequestMiddleware> logger)
    {
        _logger = logger;
        _timer = new Stopwatch();
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        _logger.LogInformation("[{@Timestamp} | HTTP {@RequestType}] Handling '{@Request}'",
            DateTime.UtcNow.ToString("s"),
            context.Request.Method,
            context.Request.Path.Value);

        _timer.Start();
        await next(context);
        _timer.Stop();

        _logger.LogInformation("[{@Timestamp} | HTTP {@RequestType}({@RequestCode})] Handled '{@Request}' in {@Elapsed}ms",
            DateTime.UtcNow.ToString("s"),
            context.Request.Method,
            context.Response.StatusCode,
            context.Request.Path.Value,
            _timer.ElapsedMilliseconds);
    }
}