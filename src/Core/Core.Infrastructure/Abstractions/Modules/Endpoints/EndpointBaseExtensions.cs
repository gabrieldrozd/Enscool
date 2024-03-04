using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Core.Infrastructure.Abstractions.Modules.Endpoints;

public static class EndpointBaseExtensions
{
    public static RouteHandlerBuilder MapGetEndpoint(
        this IEndpointRouteBuilder endpointRouteBuilder,
        EndpointTag tag,
        string pattern,
        Delegate handler
    ) => endpointRouteBuilder
        .MapGet(pattern, handler)
        .ConfigureEndpoint(tag);

    public static RouteHandlerBuilder MapPostEndpoint(
        this IEndpointRouteBuilder endpointRouteBuilder,
        EndpointTag tag,
        string pattern,
        Delegate handler
    ) => endpointRouteBuilder
        .MapPost(pattern, handler)
        .ConfigureEndpoint(tag);

    public static RouteHandlerBuilder MapPutEndpoint(
        this IEndpointRouteBuilder endpointRouteBuilder,
        EndpointTag tag,
        string pattern,
        Delegate handler
    ) => endpointRouteBuilder
        .MapPut(pattern, handler)
        .ConfigureEndpoint(tag);

    public static RouteHandlerBuilder MapPatchEndpoint(
        this IEndpointRouteBuilder endpointRouteBuilder,
        EndpointTag tag,
        string pattern,
        Delegate handler
    ) => endpointRouteBuilder
        .MapPatch(pattern, handler)
        .ConfigureEndpoint(tag);

    public static RouteHandlerBuilder MapDeleteEndpoint(
        this IEndpointRouteBuilder endpointRouteBuilder,
        EndpointTag tag,
        string pattern,
        Delegate handler
    ) => endpointRouteBuilder
        .MapDelete(pattern, handler)
        .ConfigureEndpoint(tag);

    /// <summary>
    /// Configures the endpoint tag and open api.
    /// </summary>
    /// <param name="builder">The <see cref="RouteHandlerBuilder"/>.</param>
    /// <param name="tag">The <see cref="EndpointTag"/>.</param>
    /// <returns>The <see cref="RouteHandlerBuilder"/> with configuration applied.</returns>
    private static RouteHandlerBuilder ConfigureEndpoint(this RouteHandlerBuilder builder, EndpointTag tag)
    {
        builder.WithTags(tag.Value);
        builder.WithOpenApi();
        return builder;
    }
}