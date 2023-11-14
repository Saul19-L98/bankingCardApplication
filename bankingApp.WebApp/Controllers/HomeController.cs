using bankingApp.WebApp.Models;
using bankingApp.WebApp.Models.ViewModels.AccountBalanceViewModels;
using bankingApp.WebApp.Repositories.AccountBalanceRepository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace bankingApp.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAccountBalanceRepository accountBalanceRepository;

        public HomeController(ILogger<HomeController> logger,IAccountBalanceRepository accountBalanceRepository)
        {
            _logger = logger;
            this.accountBalanceRepository = accountBalanceRepository;
        }

        public async Task<IActionResult> Index()
        {
            var cardAccountDetails = await accountBalanceRepository.GetAccountDetailsAsync();
            var allPurchasesOfTheMonth = await accountBalanceRepository.GetAllPurchasesOfTheMonthAsync();
            var totalPurchasesCurrentAndPreviousMonth = await accountBalanceRepository.GetTotalPurchasesCurrentAndPreviousMonthAsyn();

            if (cardAccountDetails != null && 
                allPurchasesOfTheMonth != null && 
                totalPurchasesCurrentAndPreviousMonth != null)
            {
                var accountDetailsViewModel = new AccountDetailsViewModel
                {
                    CardAccountDetails = cardAccountDetails,
                    Purchases = allPurchasesOfTheMonth,
                    TotalPurchasesCurrentAndPreviousMonth = totalPurchasesCurrentAndPreviousMonth,
                };

                return View(accountDetailsViewModel);
            }
            return View(null);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}