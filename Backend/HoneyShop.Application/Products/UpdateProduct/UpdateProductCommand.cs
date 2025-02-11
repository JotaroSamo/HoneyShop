using HoneyShop.Application.Core.Commands.Contracts;
using HoneyShop.Model.Models.Product;

namespace HoneyShop.Application.Products.UpdateProduct;

public class UpdateProductCommand : ICommand<ProductItem>
{
    public Model.Models.Product.UpdateProduct UpdateProduct { get; }

    public UpdateProductCommand(Model.Models.Product.UpdateProduct updateProduct)
    {
        UpdateProduct = updateProduct;
    }
}