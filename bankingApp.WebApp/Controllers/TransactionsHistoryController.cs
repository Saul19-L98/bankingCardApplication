using bankingApp.WebApp.Repositories.TransactionsHistoryRepository;
using Microsoft.AspNetCore.Mvc;

namespace bankingApp.WebApp.Controllers;

public class TransactionsHistoryController : Controller
{
    private readonly ITransactionsHistoryRepository transactionsHistoryRepository;

    public TransactionsHistoryController(ITransactionsHistoryRepository transactionsHistoryRepository)
    {
        this.transactionsHistoryRepository = transactionsHistoryRepository;
    }

    public async Task<IActionResult> Index()
    {
        var allTransactionsCurrentMonth = await transactionsHistoryRepository.GetTransactionsForCurrentMonthAsync();
        if (allTransactionsCurrentMonth == null)
        {
            return View(null);
        }
        return View(allTransactionsCurrentMonth);
    }
}
