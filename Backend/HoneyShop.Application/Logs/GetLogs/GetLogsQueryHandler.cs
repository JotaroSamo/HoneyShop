using HoneyShop.Application.Core.Queries.Contracts;
using HoneyShop.DataAccess.MongoDb;
using HoneyShop.DataAccess.MongoDb.Models;
using HoneyShop.Model.Pagination;
using Microsoft.Extensions.Logging;

namespace HoneyShop.Application.Logs.GetLogs;

public class GetLogsQueryHandler : IQueryHandler<GetLogsQuery, PaginationListModel<LogModel>>
{
    private readonly MongoContextService _logService;
    private readonly ILogger<GetLogsQueryHandler> _logger;

    public GetLogsQueryHandler(MongoContextService logService, ILogger<GetLogsQueryHandler> logger)
    {
        _logService = logService;
        _logger = logger;
    }

    public async Task<PaginationListModel<LogModel>> Handle(GetLogsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Обработка GetLogsQuery с параметрами: Поиск: {Search}, Страница: {Page}, Размер страницы: {PageSize}", request.Search, request.Page, request.PageSize);

        var logs = await _logService.SearchLogsAsync(request.Search, request.Page, request.PageSize);


        _logger.LogInformation("Получено всего логов: {TotalLogs}", logs.Total);
        

        _logger.LogInformation("Возвращаем результат с количеством логов: {LogCount}", logs.Models.Count);

        return logs;
    }
}
