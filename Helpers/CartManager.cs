using MVCShop.Areas.Admin.Models;
using MVCShop.Models.DTO;
using Newtonsoft.Json;


namespace MVCShop.Helpers;

public static class CartManager
{
    //public static void AddToCart(ISession session, Product product)
    //{
    //    List<Product> cart;
    //    try
    //    {
    //        var rawProducts = session.GetString("Cart");
    //        cart = JsonConvert.DeserializeObject<List<Product>>(rawProducts!)!;
    //    }
    //    catch (Exception)
    //    {
    //        cart = new();
    //    }
    //    cart.Add(product);
    //    session.SetString("Cart", JsonConvert.SerializeObject(cart));
    //}

    public static void AddToCart(ISession session, Product product)
    {
        List<CartItem> cart;
        var rawCart = session.GetString("Cart");

        if (!string.IsNullOrEmpty(rawCart))
        {
            cart = JsonConvert.DeserializeObject<List<CartItem>>(rawCart)!;
        }
        else
        {
            cart = new List<CartItem>();
        }

        var existingItem = cart.FirstOrDefault(c => c.Product.Id == product.Id);
        if (existingItem != null)
        {
            existingItem.Quantity++;
        }
        else
        {
            cart.Add(new CartItem { Product = product, Quantity = 1 });
        }

        session.SetString("Cart", JsonConvert.SerializeObject(cart));
    }

    public static List<CartItem> GetCart(ISession session)
    {
        var rawCart = session.GetString("Cart");
        return string.IsNullOrEmpty(rawCart) ? new List<CartItem>()
                                             : JsonConvert.DeserializeObject<List<CartItem>>(rawCart)!;
    }

    public static decimal GetTotalPrice(ISession session)
    {
        var cart = GetCart(session);
        return cart.Sum(c => c.Product.Price * c.Quantity);
    }

    public static void RemoveFromCart(ISession session, int productId)
    {
        var cart = GetCart(session);
        var item = cart.FirstOrDefault(c => c.Product.Id == productId);
        if (item != null)
        {
            cart.Remove(item);
            session.SetString("Cart", JsonConvert.SerializeObject(cart));
        }
    }

    public static void DecreaseQuantity(ISession session, int productId)
    {
        var cart = GetCart(session);
        var item = cart.FirstOrDefault(c => c.Product.Id == productId);
        if (item != null)
        {
            if (item.Quantity > 1)
            {
                item.Quantity--; 
            }
            else
            {
                cart.Remove(item); 
            }

            session.SetString("Cart", JsonConvert.SerializeObject(cart));
        }
    }




}
