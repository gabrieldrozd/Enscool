using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Queries.Browse.Search;

public static class QueryableSearchExtensions
{
    /// <summary>
    /// Applies dynamic search to the given query using the specified search query, search pattern, and property selectors.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="query">The query to apply the dynamic search to.</param>
    /// <param name="searchModel">The search model containing the search query and search pattern.</param>
    /// <param name="propertySelectors">The property selectors specifying the properties to search on.</param>
    /// <returns>The query with the dynamic search applied or the original query if the search query is null or whitespace.</returns>
    public static IQueryable<TEntity> WithDynamicSearch<TEntity>(
        this IQueryable<TEntity> query,
        SearchModel? searchModel,
        params Expression<Func<TEntity, string?>>[] propertySelectors
    ) where TEntity : class
    {
        if (string.IsNullOrWhiteSpace(searchModel?.SearchValue))
            return query;

        var entityParameter = Expression.Parameter(typeof(TEntity), "e");
        var finalPredicate = propertySelectors
            .Select(selector => ConvertToObjectSelector(selector, entityParameter))
            .Select(convertedSelector => BuildLikePredicate(convertedSelector, searchModel.SearchValue, searchModel.SearchPattern))
            .Aggregate<Expression, Expression?>(default, (current, likePredicate) => current is not null
                ? Expression.OrElse(current, likePredicate)
                : likePredicate);

        if (finalPredicate == null)
            return query;

        var lambda = Expression.Lambda<Func<TEntity, bool>>(finalPredicate, entityParameter);
        return query.Where(lambda);
    }

    /// <summary>
    /// Converts a lambda expression that selects a string property of an entity to an expression that selects an object property.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity being selected.</typeparam>
    /// <param name="selector">The lambda expression that selects a string property.</param>
    /// <param name="parameter">The parameter expression to be rebound in the converted expression.</param>
    /// <returns>An expression that selects an object property.</returns>
    private static Expression ConvertToObjectSelector<TEntity>(Expression<Func<TEntity, string?>> selector, ParameterExpression parameter)
    {
        var memberAccess = RemoveConvert(selector.Body);
        return new RebindParameterExpressionVisitor(selector.Parameters[0], parameter).Visit(memberAccess);
    }

    /// <summary>
    /// Removes any convert expressions from the given expression.
    /// </summary>
    /// <param name="expression">The expression to remove convert expressions from.</param>
    /// <returns>The modified expression without any convert expressions.</returns>
    private static Expression RemoveConvert(Expression expression)
    {
        while (expression.NodeType is ExpressionType.Convert or ExpressionType.ConvertChecked)
            expression = ((UnaryExpression) expression).Operand;

        return expression;
    }

    /// <summary>
    /// Builds a LIKE predicate expression for a given property expression, search query, and search pattern.
    /// </summary>
    /// <param name="propertyExpression">The expression representing the property to search.</param>
    /// <param name="searchQuery">The search query string.</param>
    /// <param name="searchPattern">The search pattern to use (StartsWith, EndsWith, Contains).</param>
    /// <returns>An expression representing the LIKE predicate.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the 'Like' method is not found.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the search pattern is unknown.</exception>
    private static MethodCallExpression BuildLikePredicate(Expression propertyExpression, string searchQuery, SearchPattern? searchPattern)
    {
        if (propertyExpression.Type != typeof(string))
            propertyExpression = Expression.Convert(propertyExpression, typeof(string));

        const string likeMethodName = nameof(DbFunctionsExtensions.Like);
        var likeMethod = typeof(DbFunctionsExtensions).GetMethod(likeMethodName, [typeof(DbFunctions), typeof(string), typeof(string)]);
        if (likeMethod == null) throw new InvalidOperationException("Unable to find 'Like' method.");

        var pattern = searchPattern switch
        {
            SearchPattern.StartsWith => $"{searchQuery}%",
            SearchPattern.EndsWith => $"%{searchQuery}",
            SearchPattern.Contains => $"%{searchQuery}%",
            _ => $"%{searchQuery}%"
        };

        return Expression.Call(null, likeMethod, Expression.Constant(EF.Functions), propertyExpression, Expression.Constant(pattern, typeof(string)));
    }

    /// <summary>
    /// This class represents a visitor that is used to rebind parameter expressions in an expression tree.
    /// A parameter expression is rebound by replacing it with a new parameter expression.
    /// </summary>
    private sealed class RebindParameterExpressionVisitor : ExpressionVisitor
    {
        private readonly ParameterExpression _oldParameter;
        private readonly ParameterExpression _newParameter;

        public RebindParameterExpressionVisitor(ParameterExpression oldParameter, ParameterExpression newParameter)
        {
            this._oldParameter = oldParameter;
            this._newParameter = newParameter;
        }

        /// <summary>
        /// Visits a parameter expression node.
        /// </summary>
        /// <param name="node">The parameter expression node to visit.</param>
        /// <returns>Returns the visited expression node.</returns>
        protected override Expression VisitParameter(ParameterExpression node)
            => node != _oldParameter ? base.VisitParameter(node) : _newParameter;
    }
}