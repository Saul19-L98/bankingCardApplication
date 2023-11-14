using Microsoft.AspNetCore.Mvc;

namespace bankingApp.WebApp.Controllers;

public class PaymentController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
