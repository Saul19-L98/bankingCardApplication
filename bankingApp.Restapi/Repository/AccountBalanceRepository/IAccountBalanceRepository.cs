using bankingApp.Restapi.Models.Domain;
using bankingApp.Restapi.Models.DTO.AccountBalanceDTOs;

namespace bankingApp.Restapi.Repository.AccountBalanceRepository;

public interface IAccountBalanceRepository
{
    // Retrieves the cardholder's name and card number for the card
    Task<CardAccountDetailsDTO?> GetCardAccountDetails();

    // Fetches the current balance, credit limit, and available balance for a specific card
    Task<List<PurchaseDTO>?> GetPurchasesForCurrentMonth();

    // Lists purchases made with a specific credit card in the current and previous month
    Task<TotalPurchasesCurrentAndPreviousMonthDTO> GetTotalPurchasesCurrentAndPreviousMonth();
}
