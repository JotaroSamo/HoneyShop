using HoneyShop.Application.Core.Queries.Contracts;
using HoneyShop.Core.Contracts.Product;
using HoneyShop.Model.Models.Product;
using HoneyShop.Model.Pagination;
using Microsoft.Extensions.Logging;

namespace HoneyShop.Application.Products.GetProductsPage;

public class GetProductsPageQueryHandler : IQueryHandler<GetProductsPageQuery, PaginationListModel<ProductItem>>
{
    private readonly IProductService _productService;
    private readonly ILogger<GetProductsPageQueryHandler> _logger;

    public GetProductsPageQueryHandler(IProductService productService, ILogger<GetProductsPageQueryHandler> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    public async Task<PaginationListModel<ProductItem>> Handle(GetProductsPageQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Получение страницы продуктов: страница {Page}, размер страницы {PageSize}", request.Page, request.PageSize);
        var result = await _productService.GetProductPage(request.Page, request.PageSize);
        _logger.LogInformation("Страница продуктов успешно получена: страница {Page}, размер страницы {PageSize}", request.Page, request.PageSize);
        return result;
    }
}