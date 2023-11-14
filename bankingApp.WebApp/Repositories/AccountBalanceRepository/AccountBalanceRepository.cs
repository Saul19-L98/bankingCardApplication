using bankingApp.WebApp.Models.DTOs.AccountBalanceDTOs;

namespace bankingApp.WebApp.Repositories.AccountBalanceRepository;

public class AccountBalanceRepository : IAccountBalanceRepository
{
    private readonly HttpClient httpClient;

    public AccountBalanceRepository(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<CardAccountDetailsDTO?> GetAccountDetailsAsync()
    {
        var response = await httpClient.GetFromJsonAsync<CardAccountDetailsDTO>("api/AccountBalance/accountDetails");
        if (response != null)
        {
            return response;
        }
        return null;
    }

    public async Task<List<PurchaseDTO>?> GetAllPurchasesOfTheMonthAsync()
    {
        var response = await httpClient.GetFromJsonAsync<List<PurchaseDTO>>("api/AccountBalance/purchasesForCurrentMonth");
        if (response != null)
        {
            return response;
        }
        return null;
    }

    public async Task<TotalPurchasesCurrentAndPreviousMonthDTO?> GetTotalPurchasesCurrentAndPreviousMonthAsyn()
    {
        var response = await httpClient.GetFromJsonAsync<TotalPurchasesCurrentAndPreviousMonthDTO>("api/AccountBalance/totalPurchases");
        if (response != null)
        {
            return response;
        }
        return null;
    }
}
