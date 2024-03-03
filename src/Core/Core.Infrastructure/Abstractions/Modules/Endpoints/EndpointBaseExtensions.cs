using Common.Utilities.Primitives.Envelope;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Core.Infrastructure.Abstractions.Modules.Endpoints;

public static class EndpointBaseExtensions
{
    public static void ProducesEnvelope<TResult>(this RouteHandlerBuilder endpoint, int statusCode)
    {
        endpoint
            .Produces<Envelope<TResult>>(statusCode)
            .Produces<EmptyEnvelope>(StatusCodes.Status400BadRequest)
            .Produces<EmptyEnvelope>(StatusCodes.Status401Unauthorized)
            .Produces<EmptyEnvelope>(StatusCodes.Status404NotFound)
            .Produces<EmptyEnvelope>(StatusCodes.Status500InternalServerError);
    }

    public static void ProducesEnvelope(this RouteHandlerBuilder endpoint, int statusCode)
    {
        endpoint
            .Produces<Envelope>(statusCode)
            .Produces<EmptyEnvelope>(StatusCodes.Status400BadRequest)
            .Produces<EmptyEnvelope>(StatusCodes.Status401Unauthorized)
            .Produces<EmptyEnvelope>(StatusCodes.Status404NotFound)
            .Produces<EmptyEnvelope>(StatusCodes.Status500InternalServerError);
    }

    #region GET

    public static RouteHandlerBuilder MapGetEndpoint<TResult>(
        this IEndpointRouteBuilder endpointRouteBuilder,
        string pattern,
        Delegate handler)
    {
        var builder = endpointRouteBuilder.MapGet(pattern, handler);
        builder.ProducesEnvelope<TResult>(StatusCodes.Status200OK);
        builder.WithOpenApi();
        return builder;
    }

    public static RouteHandlerBuilder MapGetEndpoint(
        this IEndpointRouteBuilder endpointRouteBuilder,
        string pattern,
        Delegate handler)
    {
        var builder = endpointRouteBuilder.MapGet(pattern, handler);
        builder.ProducesEnvelope(StatusCodes.Status200OK);
        builder.WithOpenApi();
        return builder;
    }

    #endregion

    #region POST

    public static RouteHandlerBuilder MapPostEndpoint<TResult>(
        this IEndpointRouteBuilder endpointRouteBuilder,
        string pattern,
        Delegate handler)
    {
        var builder = endpointRouteBuilder.MapPost(pattern, handler);
        builder.ProducesEnvelope<TResult>(StatusCodes.Status201Created);
        builder.WithOpenApi();
        return builder;
    }

    public static RouteHandlerBuilder MapPostEndpoint(
        this IEndpointRouteBuilder endpointRouteBuilder,
        string pattern,
        Delegate handler)
    {
        var builder = endpointRouteBuilder.MapPost(pattern, handler);
        builder.ProducesEnvelope(StatusCodes.Status201Created);
        builder.WithOpenApi();
        return builder;
    }

    #endregion

    #region PUT

    public static RouteHandlerBuilder MapPutWithEnvelope<TResult>(
        this IEndpointRouteBuilder endpointRouteBuilder,
        string pattern,
        Delegate handler)
    {
        var builder = endpointRouteBuilder.MapPut(pattern, handler);
        builder.ProducesEnvelope<TResult>(StatusCodes.Status200OK);
        builder.WithOpenApi();
        return builder;
    }

    public static RouteHandlerBuilder MapPutWithEnvelope(
        this IEndpointRouteBuilder endpointRouteBuilder,
        string pattern,
        Delegate handler)
    {
        var builder = endpointRouteBuilder.MapPut(pattern, handler);
        builder.ProducesEnvelope(StatusCodes.Status200OK);
        builder.WithOpenApi();
        return builder;
    }

    #endregion

    #region PATCH

    public static RouteHandlerBuilder MapPatchEndpoint<TResult>(
        this IEndpointRouteBuilder endpointRouteBuilder,
        string pattern,
        Delegate handler)
    {
        var builder = endpointRouteBuilder.MapPatch(pattern, handler);
        builder.ProducesEnvelope<TResult>(StatusCodes.Status200OK);
        builder.WithOpenApi();
        return builder;
    }

    public static RouteHandlerBuilder MapPatchEndpoint(
        this IEndpointRouteBuilder endpointRouteBuilder,
        string pattern,
        Delegate handler)
    {
        var builder = endpointRouteBuilder.MapPatch(pattern, handler);
        builder.ProducesEnvelope(StatusCodes.Status200OK);
        builder.WithOpenApi();
        return builder;
    }

    #endregion

    #region DELETE

    public static RouteHandlerBuilder MapDeleteEndpoint<TResult>(
        this IEndpointRouteBuilder endpointRouteBuilder,
        string pattern,
        Delegate handler)
    {
        var builder = endpointRouteBuilder.MapDelete(pattern, handler);
        builder.ProducesEnvelope<TResult>(StatusCodes.Status204NoContent);
        builder.WithOpenApi();
        return builder;
    }

    public static RouteHandlerBuilder MapDeleteEndpoint(
        this IEndpointRouteBuilder endpointRouteBuilder,
        string pattern,
        Delegate handler)
    {
        var builder = endpointRouteBuilder.MapDelete(pattern, handler);
        builder.ProducesEnvelope(StatusCodes.Status204NoContent);
        builder.WithOpenApi();
        return builder;
    }

    #endregion
}