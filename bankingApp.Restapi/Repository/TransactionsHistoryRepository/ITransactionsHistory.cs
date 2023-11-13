using bankingApp.Restapi.Models.DTO.TransactionsDetailsDTOs;

namespace bankingApp.Restapi.Repository.TransactionsHistoryRepository;

public interface ITransactionsHistory
{
    Task<List<TransactionsHistoryDTO>> GetTransactionsForCurrentMonth();
}
