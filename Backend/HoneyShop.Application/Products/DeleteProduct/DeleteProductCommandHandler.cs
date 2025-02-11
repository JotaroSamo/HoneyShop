using HoneyShop.Application.Core.Commands.Contracts;
using HoneyShop.Core.Contracts.Http;
using HoneyShop.Core.Contracts.Product;
using HoneyShop.Core.Excpetions;
using Microsoft.Extensions.Logging;

namespace HoneyShop.Application.Products.DeleteProduct;

public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, bool>
{
    private readonly IProductService _productService;
    private readonly IHttpContextService _httpContextService;
    private readonly ILogger<DeleteProductCommandHandler> _logger;

    public DeleteProductCommandHandler(IProductService productService, IHttpContextService httpContextService,
        ILogger<DeleteProductCommandHandler> logger)
    {
        _productService = productService;
        _httpContextService = httpContextService;
        _logger = logger;
    }
    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var updateUserId = _httpContextService.GetCurrentUserId();
        if (updateUserId == null)
        {
            _logger.LogError("Недостаточно прав для изменения продукта с ID {ProductId}", request.Id);
            throw new HoneyException("Недостаточно прав", 403);
        }

        return await _productService.Delete(request.Id, updateUserId.Value);
    }
}