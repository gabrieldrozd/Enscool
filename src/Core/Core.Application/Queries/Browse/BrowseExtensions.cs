using System.Linq.Expressions;
using Common.Utilities.Abstractions.Mapping;
using Core.Application.Queries.Browse.Extensions;
using Core.Domain.Primitives;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Queries.Browse;

public static class BrowseExtensions
{
    public static async Task<BrowseResult<TEntity>> BrowseAsync<TEntity>(
        this IQueryable<TEntity> query,
        BrowseModel? browseModel,
        Expression<Func<TEntity, string?>>[] propertySelectors,
        CancellationToken cancellationToken = default
    )
        where TEntity : class, IEntity
    {
        var model = browseModel ?? BrowseModel.Default;
        query = query.WithDynamicSearch(model.Search, model.Pattern, propertySelectors);

        var totalItems = await query.CountAsync(cancellationToken);

        var items = await query
            .WithDynamicSort(model.Sort, model.Order)
            .WithPagination(model.Page, model.Size)
            .ToListAsync(cancellationToken);

        return BrowseResult<TEntity>.Create(model.Page, model.Size, totalItems, items);
    }

    // public static async Task<BrowseResult<TEntityDto>> MapResult<TEntity, TEntityDto>(
    //     this Task<BrowseResult<TEntity>> browseTask
    // )
    //     where TEntity : class, IEntity
    //     where TEntityDto : class, IWithMapFrom<TEntity, TEntityDto>
    // {
    //     var browseResult = await browseTask;
    //     return browseResult.MapTo<TEntityDto>();
    // }

    // TODO: Find a way to have both mapping methods: one for IWithMapFrom and one for IWithExpressionMapFrom
    public static async Task<BrowseResult<TEntityDto>> MapResult<TEntity, TEntityDto>(
        this Task<BrowseResult<TEntity>> browseTask
    )
        where TEntity : class, IEntity
        where TEntityDto : class, IWithExpressionMapFrom<TEntity, TEntityDto>
    {
        var browseResult = await browseTask;
        return browseResult.MapTo<TEntityDto>();
    }
}