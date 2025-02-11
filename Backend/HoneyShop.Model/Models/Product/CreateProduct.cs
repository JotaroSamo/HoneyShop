using System.ComponentModel.DataAnnotations;
using HoneyShop.Model.Enums;

namespace HoneyShop.Model.Models.Product;


public class CreateProduct
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(100)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [Range(0, 1000000000)]
    public decimal Price { get; set; }

    public ProductStatusEnum Status { get; set; }
    public List<int> Files { get; set; }
}