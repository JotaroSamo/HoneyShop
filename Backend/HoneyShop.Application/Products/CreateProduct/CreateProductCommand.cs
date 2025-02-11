using HoneyShop.Application.Core.Commands.Contracts;
using HoneyShop.Model.Models.Product;

namespace HoneyShop.Application.Products.CreateProduct;

public class CreateProductCommand : ICommand<ProductItem>
{
    public Model.Models.Product.CreateProduct CreateProduct { get; }

    public CreateProductCommand(Model.Models.Product.CreateProduct createProduct)
    {
        CreateProduct = createProduct;
    }
}