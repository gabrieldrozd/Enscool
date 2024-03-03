using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Core.Infrastructure.Abstractions.Modules.Endpoints.Extensions;

public static class PatchEndpointExtensions
{
    public static RouteHandlerBuilder MapPatchEndpoint<TResult>(
        this IEndpointRouteBuilder endpointRouteBuilder,
        EndpointTag tag,
        string pattern,
        Delegate handler)
    {
        var builder = endpointRouteBuilder
            .MapPatch(pattern, handler);

        builder.WithTags(tag.Value);
        builder.ProducesEnvelope<TResult>(StatusCodes.Status200OK);
        builder.WithOpenApi();
        return builder;
    }

    public static RouteHandlerBuilder MapPatchEndpoint(
        this IEndpointRouteBuilder endpointRouteBuilder,
        EndpointTag tag,
        string pattern,
        Delegate handler)
    {
        var builder = endpointRouteBuilder
            .MapPatch(pattern, handler);

        builder.WithTags(tag.Value);
        builder.ProducesEnvelope(StatusCodes.Status200OK);
        builder.WithOpenApi();
        return builder;
    }
}