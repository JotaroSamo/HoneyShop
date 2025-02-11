using HoneyShop.Application.Core.Queries.Contracts;
using HoneyShop.Model.Models.Product;

namespace HoneyShop.Application.Products.GetProductById;

public class GetProductByIdQuery : IQuery<ProductItem>
{
    public int Id { get; }

    public GetProductByIdQuery(int id)
    {
        Id = id;
    }
}