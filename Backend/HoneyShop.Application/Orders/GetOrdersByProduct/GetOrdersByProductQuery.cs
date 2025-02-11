using HoneyShop.Application.Core.Queries.Contracts;
using HoneyShop.Model.Models.Order;
using HoneyShop.Model.Pagination;

namespace HoneyShop.Application.Orders.GetOrdersByProduct;

public class GetOrdersByProductQuery : IQuery<PaginationListModel<OrderItem>>
{
    public int ProductId { get; }
    public int Page { get; }
    public int PageSize { get; }

    public GetOrdersByProductQuery(int productId, int page, int pageSize)
    {
        ProductId = productId;
        Page = page;
        PageSize = pageSize;
    }
}