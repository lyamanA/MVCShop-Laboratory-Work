using MVCShop.Areas.Admin.Models;

namespace MVCShop.Models.DTO;

public class ProductsCategoriesDto
{
    public List<Product> Products{ get; set; }
    public List<Category> Categories{ get; set; }

    public ProductsCategoriesDto(List<Product> products, List<Category> categories)
    {
        Products = products;
        Categories = categories;
    }
}
