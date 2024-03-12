using Common.Utilities.Primitives.Results;
using Common.Utilities.Primitives.Results.Extensions;
using Microsoft.AspNetCore.Routing;

namespace Core.Infrastructure.Cores.Modules.Endpoints;

/// <summary>
/// Represents an endpoint in the application.
/// </summary>
public abstract class EndpointBase
{
    /// <summary>
    /// Adds the routes to the specified <see cref="IEndpointRouteBuilder"/>.
    /// </summary>
    #pragma warning disable S927
    public abstract void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder);
    #pragma warning restore S927

    /// <summary>
    /// Builds the <see cref="EnvelopeResult"/> from the specified <see cref="Result"/>.
    /// </summary>
    /// <param name="result">Non-generic <see cref="Result"/>.</param>
    /// <returns>The <see cref="EnvelopeResult"/>.</returns>
    protected static EnvelopeResult BuildEnvelope(Result result)
    {
        var envelope = result.ToEnvelope();
        return new EnvelopeResult(envelope);
    }

    /// <summary>
    /// Builds the <see cref="EnvelopeResult{T}"/> from the specified <see cref="Result{T}"/>.
    /// </summary>
    /// <param name="result">Generic <see cref="Result{T}"/>.</param>
    /// <typeparam name="T">The type of the result.</typeparam>
    /// <returns>The <see cref="EnvelopeResult{T}"/>.</returns>
    protected static EnvelopeResult<T> BuildEnvelope<T>(Result<T> result)
    {
        var envelope = result.ToEnvelope();
        return new EnvelopeResult<T>(envelope);
    }
}