using bankingApp.Restapi.Models.Domain;
using bankingApp.Restapi.Repository.AccountBalanceRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bankingApp.Restapi.Controllers.AccountStatus;

[Route("api/[controller]")]
[ApiController]
public class AccountBalanceController : ControllerBase
{
    private readonly IAccountBalanceRepository accountBalanceRepository;

    public AccountBalanceController( IAccountBalanceRepository accountBalanceRepository)
    {
        this.accountBalanceRepository = accountBalanceRepository;
    }

    [HttpGet]
    [Route("accountDetails")]
    public async Task<IActionResult> GetAccountDetails()
    {
        var accountDetails = await accountBalanceRepository.GetCardAccountDetails();

        if (accountDetails != null)
        {
            return Ok(accountDetails);
        }
        return NotFound(new { Message = "Account details not found."});
    }
    [HttpGet]
    [Route("purchasesForCurrentMonth")]
    public async Task<IActionResult> GetPurchasesForCurrentMonth()
    {
        var purchasesForCurrentMonth = await accountBalanceRepository.GetPurchasesForCurrentMonth();
        if (purchasesForCurrentMonth != null)
        {
            return Ok(purchasesForCurrentMonth);
        }
        return NotFound(new { Message = "Currently there are no purchases made." });
    }
    [HttpGet]
    [Route("totalPurchases")]
    public async Task<IActionResult> GetTotalPurchasesCurrentAndPreviousMonth()
    {
        var totalPurchasesCurrentAndPreviousMonth = await accountBalanceRepository.GetTotalPurchasesCurrentAndPreviousMonth();
    
        return Ok(totalPurchasesCurrentAndPreviousMonth);  
    }
}
