using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Core.Infrastructure.Abstractions.Modules.Endpoints.Extensions;

public static class PostEndpointExtensions
{
    public static RouteHandlerBuilder MapPostEndpoint<TResult>(
        this IEndpointRouteBuilder endpointRouteBuilder,
        EndpointTag tag,
        string pattern,
        Delegate handler)
    {
        var builder = endpointRouteBuilder
            .MapPost(pattern, handler);

        builder.WithTags(tag.Value);
        builder.ProducesEnvelope<TResult>(StatusCodes.Status201Created);
        builder.WithOpenApi();
        return builder;
    }

    public static RouteHandlerBuilder MapPostEndpoint(
        this IEndpointRouteBuilder endpointRouteBuilder,
        EndpointTag tag,
        string pattern,
        Delegate handler)
    {
        var builder = endpointRouteBuilder
            .MapPost(pattern, handler);

        builder.WithTags(tag.Value);
        builder.ProducesEnvelope(StatusCodes.Status201Created);
        builder.WithOpenApi();
        return builder;
    }
}