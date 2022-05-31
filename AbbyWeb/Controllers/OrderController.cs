using Microsoft.AspNetCore.Mvc;

namespace AbbyWeb.Controllers;

public class OrderController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }

}