using HoneyShop.Application.Core.Queries.Contracts;
using HoneyShop.Model.Models.Order;
using HoneyShop.Model.Pagination;

namespace HoneyShop.Application.Orders.GetOrders;

public class GetOrdersQuery : IQuery<PaginationListModel<OrderItem>>
{
    public int Page { get; }
    public int PageSize { get; }

    public GetOrdersQuery(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }
}