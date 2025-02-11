using HoneyShop.Application.Core.Queries.Contracts;
using HoneyShop.DataAccess.MongoDb.Models;
using HoneyShop.Model.Pagination;

namespace HoneyShop.Application.Logs.GetLogs;

public class GetLogsQuery : IQuery<PaginationListModel<LogModel>>
{
    public string? Search { get; }
    public int Page { get; }
    public int PageSize { get; }

    public GetLogsQuery(string? search, int page, int pageSize)
    {
        Search = search;
        Page = page;
        PageSize = pageSize;
    }
}