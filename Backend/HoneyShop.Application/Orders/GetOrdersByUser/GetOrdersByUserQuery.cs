using HoneyShop.Application.Core.Queries.Contracts;
using HoneyShop.Model.Models.Order;
using HoneyShop.Model.Pagination;

namespace HoneyShop.Application.Orders.GetOrdersByUser;

public class GetOrdersByUserQuery : IQuery<PaginationListModel<OrderItem>>
{
    public int Page { get; }
    public int PageSize { get; }

    public GetOrdersByUserQuery(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }
}