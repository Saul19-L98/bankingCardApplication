using bankingApp.WebApp.Models.DTOs.AccountBalanceDTOs;
using bankingApp.WebApp.Models.ViewModels.AccountBalanceViewModels;

namespace bankingApp.WebApp.Repositories.AccountBalanceRepository;

public interface IAccountBalanceRepository
{
    Task<CardAccountDetailsDTO?> GetAccountDetailsAsync();
    Task<List<PurchaseDTO>?> GetAllPurchasesOfTheMonthAsync();
    Task<TotalPurchasesCurrentAndPreviousMonthDTO?> GetTotalPurchasesCurrentAndPreviousMonthAsyn();
}
