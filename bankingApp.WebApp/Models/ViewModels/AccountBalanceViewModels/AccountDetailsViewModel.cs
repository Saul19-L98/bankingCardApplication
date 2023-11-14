using bankingApp.WebApp.Models.DTOs.AccountBalanceDTOs;

namespace bankingApp.WebApp.Models.ViewModels.AccountBalanceViewModels;

public class AccountDetailsViewModel
{
    public CardAccountDetailsDTO CardAccountDetails { get; set; }
    public List<PurchaseDTO> Purchases { get; set; }
    public TotalPurchasesCurrentAndPreviousMonthDTO TotalPurchasesCurrentAndPreviousMonth { get; set; }
}
