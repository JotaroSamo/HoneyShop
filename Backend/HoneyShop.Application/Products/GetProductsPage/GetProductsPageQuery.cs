using HoneyShop.Application.Core.Queries.Contracts;
using HoneyShop.Model.Models.Product;
using HoneyShop.Model.Pagination;

namespace HoneyShop.Application.Products.GetProductsPage;

public class GetProductsPageQuery :IQuery<PaginationListModel<ProductItem>>
{
    public int Page { get; }
    public int PageSize { get; }

    public GetProductsPageQuery(int page, int pageSize)
    {
        Page = page;
        PageSize = pageSize;
    }
}