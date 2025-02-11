using System.ComponentModel.DataAnnotations;

namespace HoneyShop.Model.Models.Product;

public class UpdateProduct
{    
    [Required]
    public int Id { get; set; }
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(100)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [Range(0, 1000000000)]
    public decimal Price { get; set; }

    [Required]
    public int StatusId { get; set; }

    public List<int> Files { get; set; }
}