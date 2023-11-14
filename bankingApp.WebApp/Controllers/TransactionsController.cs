using Microsoft.AspNetCore.Mvc;

namespace bankingApp.WebApp.Controllers;

public class TransactionsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
