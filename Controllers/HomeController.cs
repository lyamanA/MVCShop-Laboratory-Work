using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MVCShop.Areas.Admin.Data;
using System.Diagnostics;
using MVCShop.Models;
using MVCShop.Models.DTO;
using MVCShop.Helpers;

namespace MVCShop.Controllers;

public class HomeController : Controller
{
    //
    private readonly AppDbContext _context;
    public HomeController(AppDbContext context) => _context = context;

    #region Get


    public IActionResult Index(int? categoryId)
    {
        var products = categoryId == null ? _context.Products.Include(p => p.Category).ToList()
                                          : _context.Products.Include(p => p.Category).Where(p => p.CategoryId == categoryId).ToList();


        var categories = _context.Categories.ToList();

        var productsCategoriesDto = new ProductsCategoriesDto(products, categories);

      

        return View(productsCategoriesDto);
    }

    public IActionResult Details(int id)
    {
        var product = _context.Products.Include(p => p.Category).FirstOrDefault(prod => prod.Id == id);
        if (product is not null)
        {
            var relatedProducts = _context.
                Products.
                Where(p => p.CategoryId == product.CategoryId && p.Id != id).ToList();

            ViewBag.RelatedProducts = relatedProducts;
        }
        return View(product);
    }

    #endregion

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult AddToCart(int id) 
    {
        var product = _context.Products.FirstOrDefault(p => p.Id == id);
        var currentSession = HttpContext.Session;
        CartManager.AddToCart(currentSession, product);
        return RedirectToAction(nameof(Index));
    }

    

    [HttpPost]
    public IActionResult RemoveFromCart(int id)
    {
        var currentSession = HttpContext.Session;
        CartManager.RemoveFromCart(currentSession, id);

        return RedirectToAction(nameof(Index)); 
    }

    [HttpPost]
    public IActionResult DecreaseQuantity(int id)
    {
        CartManager.DecreaseQuantity(HttpContext.Session, id);
        return RedirectToAction(nameof(Index));

    }



    #region Error



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    #endregion
}
