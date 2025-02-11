using HonaeyShop.Domain.Model;
using HoneyShop.Application.Core.Commands.Contracts;
using HoneyShop.Core.Contracts.Product;
using HoneyShop.Model.Models.Product;
using Microsoft.Extensions.Logging;

namespace HoneyShop.Application.Products.CreateProduct;

public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, ProductItem>
{
    private readonly IProductService _productService;
    private readonly ILogger<CreateProductCommandHandler> _logger;

    public CreateProductCommandHandler(IProductService productService, ILogger<CreateProductCommandHandler> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    public async Task<ProductItem> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Начало создания продукта с именем {ProductName}", request.CreateProduct.Name);
        var product = new Product()
        {
            Name = request.CreateProduct.Name,
            Description = request.CreateProduct.Description,
            Price = request.CreateProduct.Price,
            StatusId = (int)request.CreateProduct.Status,
            IsRemoved = false
        };
        var publicProduct = await _productService.Create(product, request.CreateProduct.Files);
        _logger.LogInformation("Продукт с именем {ProductName} успешно создан", request.CreateProduct.Name);
        return publicProduct;
    }
}
