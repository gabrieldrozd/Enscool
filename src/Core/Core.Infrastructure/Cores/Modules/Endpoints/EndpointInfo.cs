using Core.Domain.Primitives.Enumerations;

namespace Core.Infrastructure.Cores.Modules.Endpoints;

/// <summary>
/// <para>Abstract class representing a module endpoint information.</para>
/// <para>Each module should have its own endpoint information implementation.</para>
/// </summary>
public abstract record EndpointInfo : SimpleEnumeration<EndpointInfo>
{
    /// <summary>
    /// The base module path.
    /// </summary>
    public string ModulePath { get; init; } = string.Empty;

    /// <summary>
    /// The route of the endpoint in module.
    /// </summary>
    public string Route { get; init; } = string.Empty;

    /// <summary>
    /// Initializes a new instance of <see cref="EndpointInfo"/>.
    /// </summary>
    /// <param name="value">The name of the module endpoint.</param>
    /// <param name="modulePath">The base module path.</param>
    /// <param name="route">The route of the endpoint in module.</param>
    protected EndpointInfo(string value, string modulePath, string route) : base(value)
    {
        ModulePath = modulePath;
        Route = route;
    }
}