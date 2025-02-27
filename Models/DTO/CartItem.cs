using MVCShop.Areas.Admin.Models;

namespace MVCShop.Models.DTO;

public class CartItem
{
    public Product Product { get; set; }
    public int Quantity { get; set; }    


}
