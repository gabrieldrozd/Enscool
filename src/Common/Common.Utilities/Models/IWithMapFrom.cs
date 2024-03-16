namespace Common.Utilities.Models;

/// <summary>
/// Represents an interface that provides mapping functionality from a specific <typeparamref name="TSource"/> type to the <typeparamref name="TSelf"/> type.
/// </summary>
/// <typeparam name="TSource">The type from which the mapping is performed.</typeparam>
/// <typeparam name="TSelf">The type to which the mapping is performed.</typeparam>
public interface IWithMapFrom<in TSource, out TSelf>
{
    /// <summary>
    /// Maps the specified <paramref name="source"/> to the current instance.
    /// </summary>
    public static abstract TSelf From(TSource source);
}