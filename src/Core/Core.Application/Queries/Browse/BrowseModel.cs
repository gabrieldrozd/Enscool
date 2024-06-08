using Core.Application.Queries.Browse.Pagination;
using Core.Application.Queries.Browse.Search;
using Core.Application.Queries.Browse.Sort;

namespace Core.Application.Queries.Browse;

public sealed record BrowseModel(PaginationModel Pagination, SortModel? Sort = null, SearchModel? Search = null);