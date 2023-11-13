using bankingApp.Restapi.Repository.TransactionsHistoryRepository;
using Microsoft.AspNetCore.Mvc;

namespace bankingApp.Restapi.Controllers.TransactionsHistory;

[Route("api/[controller]")]
[ApiController]
public class TransactionsHistoryController : ControllerBase
{
    private readonly ITransactionsHistory transactionsHistory;

    public TransactionsHistoryController(ITransactionsHistory transactionsHistory)
    {
        this.transactionsHistory = transactionsHistory;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTransactionsForCurrentMonth()
    {
        var allTransactions = await transactionsHistory.GetTransactionsForCurrentMonth();
        if (allTransactions.Count == 0)
        {
            return NotFound(new { Message = "Currently, in this month, you have not committed any transactions." });
        }
        return Ok(allTransactions);
    }
}
