using HoneyShop.Application.Core.Queries.Contracts;
using HoneyShop.Core.Contracts.Http;
using HoneyShop.Core.Contracts.Order;
using HoneyShop.Core.Excpetions;
using HoneyShop.Model.Models.Order;
using HoneyShop.Model.Pagination;
using Microsoft.Extensions.Logging;

namespace HoneyShop.Application.Orders.GetOrdersByUser;

public class GetOrdersByUserQueryHandler : IQueryHandler<GetOrdersByUserQuery, PaginationListModel<OrderItem>>
{
    private readonly IOrderService _orderService;
    private readonly IHttpContextService _httpContextService;
    private readonly ILogger<GetOrdersByUserQueryHandler> _logger;

    public GetOrdersByUserQueryHandler(IOrderService orderService, IHttpContextService httpContextService,
        ILogger<GetOrdersByUserQueryHandler> logger)
    {
        _orderService = orderService;
        _httpContextService = httpContextService;
        _logger = logger;
    }
    public async Task<PaginationListModel<OrderItem>> Handle(GetOrdersByUserQuery request, CancellationToken cancellationToken)
    {
        var contextUserId = _httpContextService.GetCurrentUserId();
        if (contextUserId == null)
        {
            _logger.LogError("Пользователь не установлен", 403);
            throw new HoneyException("Недостаточно прав", 403);
        }
        return await _orderService.GetOrdersByUser(contextUserId.Value, request.Page, request.PageSize);
        
    }
}