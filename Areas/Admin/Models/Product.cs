using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCShop.Areas.Admin.Models;

public class Product
{
    public int Id { get; set; }

    //[MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    //[Column(TypeName =("decimal(8,2)"))]
    public decimal Price { get; set; }
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public int? Rating { get; set; }
    public int CategoryId { get; set; }

    //Nav property
    public Category? Category { get; set; }

    [NotMapped]
    public IFormFile? ImageFile { get; set; }

}
