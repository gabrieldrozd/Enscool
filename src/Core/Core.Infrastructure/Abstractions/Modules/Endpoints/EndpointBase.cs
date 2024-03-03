using Carter;
using Common.Utilities.Primitives.Results;
using Common.Utilities.Primitives.Results.Extensions;
using Microsoft.AspNetCore.Routing;

namespace Core.Infrastructure.Abstractions.Modules.Endpoints;

/// <summary>
/// Represents an endpoint in the application.
/// </summary>
public abstract class EndpointBase : ICarterModule
{
    #pragma warning disable S927
    public abstract void AddRoutes(IEndpointRouteBuilder endpointRouteBuilder);
    #pragma warning restore S927

    protected EnvelopeResult BuildEnvelope(Result result)
    {
        var envelope = result.ToEnvelope();
        return new EnvelopeResult(envelope);
    }

    protected EnvelopeResult<T> BuildEnvelope<T>(Result<T> result)
    {
        var envelope = result.ToEnvelope();
        return new EnvelopeResult<T>(envelope);
    }
}