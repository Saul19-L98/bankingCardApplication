using bankingApp.WebApp.Models.DTOs.TransactionsHistoryDTOs;

namespace bankingApp.WebApp.Repositories.TransactionsHistoryRepository;

public interface ITransactionsHistoryRepository
{
    Task<IEnumerable<TransactionsHistoryDTO>?> GetTransactionsForCurrentMonthAsync();
}
