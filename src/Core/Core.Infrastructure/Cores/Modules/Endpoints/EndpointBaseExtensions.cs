using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Core.Infrastructure.Cores.Modules.Endpoints;

public static class EndpointBaseExtensions
{
    #region MapGetEndpoint

    public static RouteHandlerBuilder MapGetEndpoint(
        this IEndpointRouteBuilder endpointRouteBuilder,
        EndpointInfo info,
        [AspMinimalApiHandler] Delegate handler
    ) => endpointRouteBuilder
        .MapGet(CreateEndpointPath(info), handler)
        .ConfigureEndpoint(info.Value);

    public static RouteHandlerBuilder MapGetEndpoint(
        this IEndpointRouteBuilder endpointRouteBuilder,
        EndpointInfo info,
        [RouteTemplate, StringSyntax("Route")] string pattern,
        [AspMinimalApiHandler] Delegate handler
    ) => endpointRouteBuilder
        .MapGet(CreateEndpointPath(info, pattern), handler)
        .ConfigureEndpoint(info.Value);

    #endregion

    #region MapPostEndpoint

    public static RouteHandlerBuilder MapPostEndpoint(
        this IEndpointRouteBuilder endpointRouteBuilder,
        EndpointInfo info,
        [AspMinimalApiHandler] Delegate handler
    ) => endpointRouteBuilder
        .MapPost(CreateEndpointPath(info), handler)
        .ConfigureEndpoint(info.Value);

    public static RouteHandlerBuilder MapPostEndpoint(
        this IEndpointRouteBuilder endpointRouteBuilder,
        EndpointInfo info,
        [RouteTemplate, StringSyntax("Route")] string pattern,
        [AspMinimalApiHandler] Delegate handler
    ) => endpointRouteBuilder
        .MapPost(CreateEndpointPath(info, pattern), handler)
        .ConfigureEndpoint(info.Value);

    #endregion

    #region MapPutEndpoint

    public static RouteHandlerBuilder MapPutEndpoint(
        this IEndpointRouteBuilder endpointRouteBuilder,
        EndpointInfo info,
        [AspMinimalApiHandler] Delegate handler
    ) => endpointRouteBuilder
        .MapPut(CreateEndpointPath(info), handler)
        .ConfigureEndpoint(info.Value);

    public static RouteHandlerBuilder MapPutEndpoint(
        this IEndpointRouteBuilder endpointRouteBuilder,
        EndpointInfo info,
        [RouteTemplate, StringSyntax("Route")] string pattern,
        [AspMinimalApiHandler] Delegate handler
    ) => endpointRouteBuilder
        .MapPut(CreateEndpointPath(info, pattern), handler)
        .ConfigureEndpoint(info.Value);

    #endregion

    #region MapPatchEndpoint

    public static RouteHandlerBuilder MapPatchEndpoint(
        this IEndpointRouteBuilder endpointRouteBuilder,
        EndpointInfo info,
        [AspMinimalApiHandler] Delegate handler
    ) => endpointRouteBuilder
        .MapPatch(CreateEndpointPath(info), handler)
        .ConfigureEndpoint(info.Value);

    public static RouteHandlerBuilder MapPatchEndpoint(
        this IEndpointRouteBuilder endpointRouteBuilder,
        EndpointInfo info,
        [RouteTemplate, StringSyntax("Route")] string pattern,
        [AspMinimalApiHandler] Delegate handler
    ) => endpointRouteBuilder
        .MapPatch(CreateEndpointPath(info, pattern), handler)
        .ConfigureEndpoint(info.Value);

    #endregion

    #region MapDeleteEndpoint

    public static RouteHandlerBuilder MapDeleteEndpoint(
        this IEndpointRouteBuilder endpointRouteBuilder,
        EndpointInfo info,
        [AspMinimalApiHandler] Delegate handler
    ) => endpointRouteBuilder
        .MapDelete(CreateEndpointPath(info), handler)
        .ConfigureEndpoint(info.Value);

    public static RouteHandlerBuilder MapDeleteEndpoint(
        this IEndpointRouteBuilder endpointRouteBuilder,
        EndpointInfo info,
        [RouteTemplate, StringSyntax("Route")] string pattern,
        [AspMinimalApiHandler] Delegate handler
    ) => endpointRouteBuilder
        .MapDelete(CreateEndpointPath(info, pattern), handler)
        .ConfigureEndpoint(info.Value);

    #endregion

    /// <summary>
    /// Configures the endpoint info and open api.
    /// </summary>
    /// <param name="builder">The <see cref="RouteHandlerBuilder"/>.</param>
    /// <param name="tag">The <see cref="EndpointInfo"/> tag.</param>
    /// <returns>The <see cref="RouteHandlerBuilder"/> with configuration applied.</returns>
    private static RouteHandlerBuilder ConfigureEndpoint(this RouteHandlerBuilder builder, string tag)
    {
        builder.WithTags(tag);
        builder.WithOpenApi();
        return builder;
    }

    #region CreateEndpointPath

    /// <summary>
    /// Creates the endpoint path from the <see cref="EndpointInfo"/>.
    /// </summary>
    /// <param name="info">The <see cref="EndpointInfo"/>.</param>
    /// <returns>The full, combined endpoint path.</returns>
    private static string CreateEndpointPath(EndpointInfo info)
        => $"{info.ModulePath.TrimEnd('/')}/{info.Route.TrimEnd('/').TrimStart('/')}";

    /// <summary>
    /// Creates the endpoint path from the <see cref="EndpointInfo"/> and pattern.
    /// </summary>
    /// <param name="info">The <see cref="EndpointInfo"/>.</param>
    /// <param name="pattern">The pattern.</param>
    /// <returns>The full, combined endpoint path.</returns>
    private static string CreateEndpointPath(EndpointInfo info, string pattern)
        => $"{info.ModulePath.TrimEnd('/')}/{info.Route.TrimEnd('/').TrimStart('/')}/{pattern.TrimStart('/')}";

    #endregion
}