using Core.Domain.Primitives.Enumerations;

namespace Core.Infrastructure.Abstractions.Modules.Endpoints;

/// <summary>
/// <para>Abstract class representing a tag for an endpoint.</para>
/// <para>Each module should have its own endpoint tag implementation.</para>
/// </summary>
public abstract record EndpointTag : SimpleEnumeration<EndpointTag>
{
    /// <summary>
    /// The route of the endpoint.
    /// </summary>
    public string Route { get; init; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of <see cref="EndpointTag"/>.
    /// </summary>
    /// <param name="value">The value of the tag.</param>
    /// <param name="route">The route of the endpoint.</param>
    protected EndpointTag(string value, string route) : base(value)
    {
        Route = route;
    }
}