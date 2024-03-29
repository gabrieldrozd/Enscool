﻿using System.Diagnostics.CodeAnalysis;
using Common.Utilities.Primitives.Envelope;
using Core.Domain.Shared.Enumerations.Roles;
using Core.Infrastructure.Auth.Api.Roles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Core.Infrastructure.Cores.Modules.Endpoints;

public static class EndpointBaseConfigurationExtensions
{
    /// <summary>
    /// Specifies required <see cref="UserRole"/>s for the endpoint.
    /// </summary>
    public static RouteHandlerBuilder RequireRoles(
        this RouteHandlerBuilder builder,
        params UserRole[] roles
    ) => builder.RequireAuthorization(new RoleRequirementAttribute(roles));

    /// <summary>
    /// Specifies that the endpoint requires <see cref="UserRole.Student"/>, <see cref="UserRole.Teacher"/>, <see cref="UserRole.Secretary"/>, <see cref="UserRole.InstitutionAdmin"/> roles.
    /// </summary>
    public static RouteHandlerBuilder RequireInstitutionRoles(this RouteHandlerBuilder builder)
        => builder.RequireAuthorization(new RoleRequirementAttribute(UserRole.Student, UserRole.Teacher, UserRole.Secretary, UserRole.InstitutionAdmin));

    /// <summary>
    /// Specifies that the endpoint requires <see cref="UserRole.Support"/>, <see cref="UserRole.BackOfficeAdmin"/>, <see cref="UserRole.GlobalAdmin"/> roles.
    /// </summary>
    public static RouteHandlerBuilder RequireBackOfficeRoles(this RouteHandlerBuilder builder)
        => builder.RequireAuthorization(new RoleRequirementAttribute(UserRole.Support, UserRole.BackOfficeAdmin, UserRole.GlobalAdmin));

    /// <summary>
    /// Adds the produces configuration.
    /// </summary>
    public static RouteHandlerBuilder ProducesEnvelope(this RouteHandlerBuilder routeHandlerBuilder, int statusCode)
    {
        routeHandlerBuilder
            .Produces<Envelope>(statusCode)
            .Produces<EmptyEnvelope>(StatusCodes.Status400BadRequest)
            .Produces<EmptyEnvelope>(StatusCodes.Status401Unauthorized)
            .Produces<EmptyEnvelope>(StatusCodes.Status404NotFound)
            .Produces<EmptyEnvelope>(StatusCodes.Status500InternalServerError);

        return routeHandlerBuilder;
    }

    /// <summary>
    /// Adds the generic produces configuration.
    /// </summary>
    public static RouteHandlerBuilder ProducesEnvelope<TResult>(this RouteHandlerBuilder routeHandlerBuilder, int statusCode)
    {
        routeHandlerBuilder
            .Produces<Envelope<TResult>>(statusCode)
            .Produces<EmptyEnvelope>(StatusCodes.Status400BadRequest)
            .Produces<EmptyEnvelope>(StatusCodes.Status401Unauthorized)
            .Produces<EmptyEnvelope>(StatusCodes.Status404NotFound)
            .Produces<EmptyEnvelope>(StatusCodes.Status500InternalServerError);

        return routeHandlerBuilder;
    }

    /// <summary>
    /// Adds the endpoint documentation.
    /// </summary>
    /// <param name="builder">The <see cref="IEndpointConventionBuilder"/>.</param>
    /// <param name="name">The name of the endpoint.</param>
    /// <param name="title">The title of the endpoint.</param>
    /// <param name="description">The description of the endpoint.</param>
    /// <param name="example">The example of the endpoint.</param>
    public static RouteHandlerBuilder WithDocumentation(
        this RouteHandlerBuilder builder,
        string name,
        string title,
        string description,
        [StringSyntax(StringSyntaxAttribute.Json)]
        string? example = null)
    {
        builder
            .WithName(name)
            .WithSummary(title)
            .WithDescription(example is not null
                ? $"""
                   {description}
                   ```
                   {example}
                   ```
                   """
                : description)
            .WithOpenApi();

        return builder;
    }
}