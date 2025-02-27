using Microsoft.AspNetCore.Mvc;

namespace MVCShop.Areas.Admin.Controllers;

[Area("Admin")]
public class HomeController : Controller
{
    //Admin/Home/Index
    public IActionResult Index()
    {
        return View();
    }
}
