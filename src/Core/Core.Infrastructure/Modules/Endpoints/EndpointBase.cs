using Carter;
using Common.Utilities.Primitives.Results;
using Common.Utilities.Primitives.Results.Extensions;
using MediatR;
using Microsoft.AspNetCore.Routing;

namespace Core.Infrastructure.Modules.Endpoints;

/// <summary>
/// Represents an endpoint in the application.
/// </summary>
public abstract class EndpointBase : ICarterModule
{
    protected ISender Sender { get; }

    protected EndpointBase(ISender sender)
    {
        Sender = sender;
    }

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