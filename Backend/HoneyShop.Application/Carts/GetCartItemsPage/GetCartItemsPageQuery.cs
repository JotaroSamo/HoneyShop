using HoneyShop.Application.Core.Queries.Contracts;
using HoneyShop.Model.Models.Cart;
using HoneyShop.Model.Pagination;

namespace HoneyShop.Application.Carts.GetCartItemsPage;

public class GetCartItemsPageQuery : IQuery<PaginationListModel<CartItem>>
{
    public int Page { get; }
    public int PageSize { get; }


    public GetCartItemsPageQuery(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }
}