using bankingApp.Restapi.Data;
using bankingApp.Restapi.Models.DTO.TransactionsDetailsDTOs;
using bankingApp.Restapi.Repository.TransactionsHistoryRepository;
using Microsoft.EntityFrameworkCore;

namespace bankingApp.Restapi.Models.DTO.TransactionsHistoryDTOs;

public class TransactionsHistory : ITransactionsHistory
{
    private readonly AccountDbContext accountDbContext;

    public TransactionsHistory(AccountDbContext accountDbContext)
    {
        this.accountDbContext = accountDbContext;
    }

    public async Task<List<TransactionsHistoryDTO>> GetTransactionsForCurrentMonth()
    {
        var transactions = new List<TransactionsHistoryDTO>();

        using var command = accountDbContext.Database.GetDbConnection().CreateCommand();
        command.CommandText = "EXEC GetTransactionsForCurrentMonth";
        accountDbContext.Database.OpenConnection();

        using var result = await command.ExecuteReaderAsync();
        while (await result.ReadAsync())
        {
            transactions.Add(new TransactionsHistoryDTO
            {
                Date = result.GetDateTime(result.GetOrdinal("Date")),
                Description = result.GetString(result.GetOrdinal("Description")),
                Amount = result.GetDecimal(result.GetOrdinal("Amount")),
                TransactionType = result.GetString(result.GetOrdinal("TransactionType"))
            });
        }

        return transactions;
    }
}
