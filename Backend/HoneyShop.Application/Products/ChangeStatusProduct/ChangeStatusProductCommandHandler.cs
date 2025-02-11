using HoneyShop.Application.Core.Commands.Contracts;
using HoneyShop.Core.Contracts.Http;
using HoneyShop.Core.Contracts.Product;
using HoneyShop.Core.Excpetions;
using HoneyShop.Model.Enums;
using HoneyShop.Model.Models.Product;
using Microsoft.Extensions.Logging;

namespace HoneyShop.Application.Products.ChangeStatusProduct;

public class ChangeStatusProductCommandHandler : ICommandHandler<ChangeStatusProductCommand, ProductItem>
{
    private readonly IProductService _productService;
    private readonly IHttpContextService _httpContextService;
    private readonly ILogger<ChangeStatusProductCommandHandler> _logger;

    public ChangeStatusProductCommandHandler(IProductService productService, IHttpContextService httpContextService, ILogger<ChangeStatusProductCommandHandler> logger)
    {
        _productService = productService;
        _httpContextService = httpContextService;
        _logger = logger;
    }

    public async Task<ProductItem> Handle(ChangeStatusProductCommand request, CancellationToken cancellationToken)
    {
        var updateUserId = _httpContextService.GetCurrentUserId();
        if (updateUserId == null)
        {
            _logger.LogError("Нет прав на изменение статуса продукта");
            throw new HoneyException("Нет прав", 403);
        }
        var productItem = await _productService.ChangeStatus(request.Id, (int)request.Status, updateUserId.Value);
        _logger.LogInformation("Статус продукта успешно изменен. ProductId: {ProductId}, Status: {Status}, UserId: {UserId}", request.Id, request.Status, updateUserId.Value);
        return productItem;
    }
}
