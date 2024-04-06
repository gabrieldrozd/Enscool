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
        EndpointInfo endpointInfo,
        [AspMinimalApiHandler] Delegate handler
    ) => endpointRouteBuilder
        .MapGet(CreateEndpointPath(endpointInfo), handler)
        .ConfigureEndpoint(endpointInfo);

    public static RouteHandlerBuilder MapGetEndpoint(
        this IEndpointRouteBuilder endpointRouteBuilder,
        [RouteTemplate, StringSyntax("Route")] string pattern,
        EndpointInfo endpointInfo,
        [AspMinimalApiHandler] Delegate handler
    ) => endpointRouteBuilder
        .MapGet(CreateEndpointPath(endpointInfo, pattern), handler)
        .ConfigureEndpoint(endpointInfo);

    #endregion

    #region MapPostEndpoint

    public static RouteHandlerBuilder MapPostEndpoint(
        this IEndpointRouteBuilder endpointRouteBuilder,
        EndpointInfo endpointInfo,
        [AspMinimalApiHandler] Delegate handler
    ) => endpointRouteBuilder
        .MapPost(CreateEndpointPath(endpointInfo), handler)
        .ConfigureEndpoint(endpointInfo);

    public static RouteHandlerBuilder MapPostEndpoint(
        this IEndpointRouteBuilder endpointRouteBuilder,
        [RouteTemplate, StringSyntax("Route")] string pattern,
        EndpointInfo endpointInfo,
        [AspMinimalApiHandler] Delegate handler
    ) => endpointRouteBuilder
        .MapPost(CreateEndpointPath(endpointInfo, pattern), handler)
        .ConfigureEndpoint(endpointInfo);

    #endregion

    #region MapPutEndpoint

    public static RouteHandlerBuilder MapPutEndpoint(
        this IEndpointRouteBuilder endpointRouteBuilder,
        EndpointInfo endpointInfo,
        [AspMinimalApiHandler] Delegate handler
    ) => endpointRouteBuilder
        .MapPut(CreateEndpointPath(endpointInfo), handler)
        .ConfigureEndpoint(endpointInfo);

    public static RouteHandlerBuilder MapPutEndpoint(
        this IEndpointRouteBuilder endpointRouteBuilder,
        [RouteTemplate, StringSyntax("Route")] string pattern,
        EndpointInfo endpointInfo,
        [AspMinimalApiHandler] Delegate handler
    ) => endpointRouteBuilder
        .MapPut(CreateEndpointPath(endpointInfo, pattern), handler)
        .ConfigureEndpoint(endpointInfo);

    #endregion

    #region MapPatchEndpoint

    public static RouteHandlerBuilder MapPatchEndpoint(
        this IEndpointRouteBuilder endpointRouteBuilder,
        EndpointInfo endpointInfo,
        [AspMinimalApiHandler] Delegate handler
    ) => endpointRouteBuilder
        .MapPatch(CreateEndpointPath(endpointInfo), handler)
        .ConfigureEndpoint(endpointInfo);

    public static RouteHandlerBuilder MapPatchEndpoint(
        this IEndpointRouteBuilder endpointRouteBuilder,
        [RouteTemplate, StringSyntax("Route")] string pattern,
        EndpointInfo endpointInfo,
        [AspMinimalApiHandler] Delegate handler
    ) => endpointRouteBuilder
        .MapPatch(CreateEndpointPath(endpointInfo, pattern), handler)
        .ConfigureEndpoint(endpointInfo);

    #endregion

    #region MapDeleteEndpoint

    public static RouteHandlerBuilder MapDeleteEndpoint(
        this IEndpointRouteBuilder endpointRouteBuilder,
        EndpointInfo endpointInfo,
        [AspMinimalApiHandler] Delegate handler
    ) => endpointRouteBuilder
        .MapDelete(CreateEndpointPath(endpointInfo), handler)
        .ConfigureEndpoint(endpointInfo);

    public static RouteHandlerBuilder MapDeleteEndpoint(
        this IEndpointRouteBuilder endpointRouteBuilder,
        [RouteTemplate, StringSyntax("Route")] string pattern,
        EndpointInfo endpointInfo,
        [AspMinimalApiHandler] Delegate handler
    ) => endpointRouteBuilder
        .MapDelete(CreateEndpointPath(endpointInfo, pattern), handler)
        .ConfigureEndpoint(endpointInfo);

    #endregion

    /// <summary>
    /// Configures the endpoint endpointInfo and open api.
    /// </summary>
    /// <param name="builder">The <see cref="RouteHandlerBuilder"/>.</param>
    /// <param name="tag">The <see cref="EndpointInfo"/> tag.</param>
    /// <returns>The <see cref="RouteHandlerBuilder"/> with configuration applied.</returns>
    private static RouteHandlerBuilder ConfigureEndpoint(this RouteHandlerBuilder builder, EndpointInfo endpointInfo)
    {
        builder.WithGroupName(endpointInfo.ModulePath);
        builder.WithTags(endpointInfo.Value);
        builder.WithOpenApi();
        return builder;
    }

    #region CreateEndpointPath

    /// <summary>
    /// Creates the endpoint path from the <see cref="EndpointInfo"/>.
    /// </summary>
    /// <param name="endpointInfo">The <see cref="EndpointInfo"/>.</param>
    /// <returns>The full, combined endpoint path.</returns>
    private static string CreateEndpointPath(EndpointInfo endpointInfo)
        => $"{endpointInfo.ModulePath.TrimEnd('/')}/{endpointInfo.Route.TrimEnd('/').TrimStart('/')}";

    /// <summary>
    /// Creates the endpoint path from the <see cref="EndpointInfo"/> and pattern.
    /// </summary>
    /// <param name="endpointInfo">The <see cref="EndpointInfo"/>.</param>
    /// <param name="pattern">The pattern.</param>
    /// <returns>The full, combined endpoint path.</returns>
    private static string CreateEndpointPath(EndpointInfo endpointInfo, string pattern)
        => $"{endpointInfo.ModulePath.TrimEnd('/')}/{endpointInfo.Route.TrimEnd('/').TrimStart('/')}/{pattern.TrimStart('/')}";

    #endregion
}