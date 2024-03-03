using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Core.Infrastructure.Abstractions.Modules.Endpoints.Extensions;

public static class DeleteEndpointExtensions
{
    public static RouteHandlerBuilder MapDeleteEndpoint<TResult>(
        this IEndpointRouteBuilder endpointRouteBuilder,
        EndpointTag tag,
        string pattern,
        Delegate handler)
    {
        var builder = endpointRouteBuilder
            .MapDelete(pattern, handler);

        builder.WithTags(tag.Value);
        builder.ProducesEnvelope<TResult>(StatusCodes.Status204NoContent);
        builder.WithOpenApi();
        return builder;
    }

    public static RouteHandlerBuilder MapDeleteEndpoint(
        this IEndpointRouteBuilder endpointRouteBuilder,
        EndpointTag tag,
        string pattern,
        Delegate handler)
    {
        var builder = endpointRouteBuilder
            .MapDelete(pattern, handler);

        builder.WithTags(tag.Value);
        builder.ProducesEnvelope(StatusCodes.Status204NoContent);
        builder.WithOpenApi();
        return builder;
    }
}