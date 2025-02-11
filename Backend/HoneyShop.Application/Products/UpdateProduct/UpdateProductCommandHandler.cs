using DnsClient.Internal;
using HoneyShop.Application.Core.Commands.Contracts;
using HoneyShop.Core.Contracts.Http;
using HoneyShop.Core.Contracts.Product;
using HoneyShop.Core.Excpetions;
using HoneyShop.Model.Models.Product;
using Microsoft.Extensions.Logging;

namespace HoneyShop.Application.Products.UpdateProduct;

public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, ProductItem>
{
    private readonly IProductService _productService;
    private readonly IHttpContextService _httpContextService;
    private readonly ILogger<UpdateProductCommandHandler> _logger;


    public UpdateProductCommandHandler(IProductService productService,IHttpContextService httpContextService 
        ,ILogger<UpdateProductCommandHandler> logger)
    {
        _productService = productService;
        _httpContextService = httpContextService;
        _logger = logger;
    }
    public async Task<ProductItem> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var updateUserId = _httpContextService.GetCurrentUserId();
        if (updateUserId == null)
        {
            _logger.LogError("Недостаточно прав для изменения продукта с ID {ProductId}", request.UpdateProduct.Id);
            throw new HoneyException("Недостаточно прав", 403);
        }

        _logger.LogInformation("Пользователь с ID {UserId} изменяет продукт с ID {ProductId}", updateUserId, request.UpdateProduct.Id);

        var updatedProduct = await _productService.Update(request.UpdateProduct, updateUserId.Value);

        _logger.LogInformation("Продукт с ID {ProductId} успешно изменен", request.UpdateProduct.Id);

        return updatedProduct;
    }

}