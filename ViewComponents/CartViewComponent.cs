using MVCShop.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MVCShop.Models.DTO;
using MVCShop.Helpers;

namespace MVCShop.ViewComponents;

public class CartViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        //var rawJson = HttpContext.Session.GetString("Cart");

        //var products = rawJson is null ? new List<Product>()
        //                               : JsonConvert.DeserializeObject<List<Product>>(rawJson);

        //var cartItems = new List<CartItem>();

        //var productsDictionary = new Dictionary<int, Product>();

        //return View(products);

        var cartItems = CartManager.GetCart(HttpContext.Session);
        return View(cartItems);
    }


}

