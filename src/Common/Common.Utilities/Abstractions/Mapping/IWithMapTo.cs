namespace Common.Utilities.Abstractions.Mapping;

/// <summary>
/// Represents an interface that provides mapping functionality to a specific <typeparamref name="TDestination"/> type.
/// </summary>
/// <typeparam name="TDestination">The type to which the mapping is performed.</typeparam>
public interface IWithMapTo<out TDestination>
{
    /// <summary>
    /// Maps the current instance to the specified <typeparamref name="TDestination"/> type.
    /// </summary>
    public TDestination Map();
}