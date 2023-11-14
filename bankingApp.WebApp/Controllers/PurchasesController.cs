using Microsoft.AspNetCore.Mvc;

namespace bankingApp.WebApp.Controllers;

public class PurchasesController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
