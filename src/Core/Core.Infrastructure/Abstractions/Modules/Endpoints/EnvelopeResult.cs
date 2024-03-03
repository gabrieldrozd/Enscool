using Common.Utilities.Primitives.Envelope;
using Microsoft.AspNetCore.Http;

namespace Core.Infrastructure.Abstractions.Modules.Endpoints;

public class EnvelopeResult : IResult
{
    private readonly Envelope _envelope;

    public EnvelopeResult(Envelope envelope)
    {
        _envelope = envelope;
    }

    public Task ExecuteAsync(HttpContext httpContext)
    {
        httpContext.Response.StatusCode = _envelope.StatusCode;
        return httpContext.Response.WriteAsJsonAsync(_envelope);
    }
}

public class EnvelopeResult<T> : IResult
{
    private readonly Envelope<T> _envelope;

    public EnvelopeResult(Envelope<T> envelope)
    {
        _envelope = envelope;
    }

    public Task ExecuteAsync(HttpContext httpContext)
    {
        httpContext.Response.StatusCode = _envelope.StatusCode;
        return httpContext.Response.WriteAsJsonAsync(_envelope);
    }
}