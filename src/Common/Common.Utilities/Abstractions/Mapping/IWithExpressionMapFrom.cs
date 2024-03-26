using System.Linq.Expressions;

namespace Common.Utilities.Abstractions.Mapping;

/// <summary>
/// Represents an interface that provides expression mapping functionality from a specific <typeparamref name="TSource"/> type to the <typeparamref name="TSelf"/> type.
/// </summary>
/// <typeparam name="TSource">The type from which the mapping is performed.</typeparam>
/// <typeparam name="TSelf">The type to which the mapping is performed.</typeparam>
public interface IWithExpressionMapFrom<TSource, TSelf>
{
    /// <summary>
    /// Gets the expression that performs the mapping from the specified <typeparamref name="TSource"/> type to the <typeparamref name="TSelf"/> type.
    /// </summary>
    static abstract Expression<Func<TSource, TSelf>> Mapper { get; }
}