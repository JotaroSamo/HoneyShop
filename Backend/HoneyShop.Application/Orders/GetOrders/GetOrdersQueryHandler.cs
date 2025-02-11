using HoneyShop.Application.Core.Queries.Contracts;
using HoneyShop.Application.Orders.GetOrdersByProduct;
using HoneyShop.Core.Contracts.Order;
using HoneyShop.Model.Models.Order;
using HoneyShop.Model.Pagination;
using Microsoft.Extensions.Logging;

namespace HoneyShop.Application.Orders.GetOrders;

public class GetOrdersQueryHandler : IQueryHandler<GetOrdersQuery, PaginationListModel<OrderItem>>
{
    private readonly IOrderService _orderService;
    private readonly ILogger<GetOrdersByProductQueryHandler> _logger;

    public GetOrdersQueryHandler(IOrderService orderService, ILogger<GetOrdersByProductQueryHandler> logger)
    {
        _orderService = orderService;
        _logger = logger;
    }
    public async Task<PaginationListModel<OrderItem>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        return await _orderService.GetOrders(request.Page, request.PageSize);
    }
}