using HoneyShop.Application.Core.Queries.Contracts;
using HoneyShop.Core.Contracts.Product;
using HoneyShop.Model.Models.Product;
using Microsoft.Extensions.Logging;

namespace HoneyShop.Application.Products.GetProductById;


    public class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, ProductItem>
    {
        private readonly IProductService _productService;
        private readonly ILogger<GetProductByIdQueryHandler> _logger;

        public GetProductByIdQueryHandler(IProductService productService, ILogger<GetProductByIdQueryHandler> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        public async Task<ProductItem> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Обработка GetProductByIdQuery с Id: {Id}", request.Id);

            var product = await _productService.GetProductById(request.Id);
            
            _logger.LogInformation("Продукт с Id: {Id} найден", request.Id);

            return product;
        }
    }

    
