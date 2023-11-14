using bankingApp.WebApp.Models.DTOs.TransactionsHistoryDTOs;

namespace bankingApp.WebApp.Repositories.TransactionsHistoryRepository;

public class TransactionsHistoryRepository : ITransactionsHistoryRepository
{
    private readonly HttpClient httpClient;

    public TransactionsHistoryRepository(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<IEnumerable<TransactionsHistoryDTO>?> GetTransactionsForCurrentMonthAsync()
    {
        var response = await httpClient.GetFromJsonAsync<IEnumerable<TransactionsHistoryDTO>>("api/TransactionsHistory");
        if (response != null)
        {
            return response;
        }
        return null;
    }
}
