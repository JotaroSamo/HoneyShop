using HoneyShop.Application.Core.Commands.Contracts;
using HoneyShop.Model.Enums;
using HoneyShop.Model.Models.Product;

namespace HoneyShop.Application.Products.ChangeStatusProduct;

public class ChangeStatusProductCommand: ICommand<ProductItem>
{
    public int Id { get; }
    public ProductStatusEnum Status { get; }

    public ChangeStatusProductCommand(int id,  ProductStatusEnum status)
    {
        Id = id;
        Status = status;
    }

    
}