using bankingApp.Restapi.Data;
using bankingApp.Restapi.Models.DTO.TransactionsDTOs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace bankingApp.Restapi.Repository.TransactionsRepository;

public class TransactionsRepository : ITransactionsRepository
{
    private readonly AccountDbContext accountDbContext;
    public TransactionsRepository(AccountDbContext accountDbContext)
    {
        this.accountDbContext = accountDbContext;
    }

    private async Task<Guid> GetSingleAccountIdAsync()
    {
        using var command = accountDbContext.Database.GetDbConnection().CreateCommand();
        command.CommandText = "EXEC GetSingleAccountId";
        accountDbContext.Database.OpenConnection();

        using var result = await command.ExecuteReaderAsync();
        if (await result.ReadAsync())
        {
            return result.GetGuid(0); // Assumes the first column is the Guid ID
        }

        throw new InvalidOperationException("No account found.");
    }

    public async Task AddPurchase(PurchaseDTO transactionDTO)
    {
        var transactionId = Guid.NewGuid();
        var accountId = await GetSingleAccountIdAsync();

        using var command = accountDbContext.Database.GetDbConnection().CreateCommand();
        command.CommandText = "EXEC AddPurchase @Id, @Date, @Description, @Amount, @AccountId";
        command.Parameters.Add(new SqlParameter("@Id", transactionId));
        command.Parameters.Add(new SqlParameter("@Date", transactionDTO.Date));
        command.Parameters.Add(new SqlParameter("@Description", transactionDTO.Description));
        command.Parameters.Add(new SqlParameter("@Amount", transactionDTO.Amount));
        command.Parameters.Add(new SqlParameter("@AccountId", accountId));

        await accountDbContext.Database.OpenConnectionAsync();
        await command.ExecuteNonQueryAsync();
    }

    public async Task MakePayment(PaymentDTO transactionDTO)
    {
        var transactionId = Guid.NewGuid();
        var accountId = await GetSingleAccountIdAsync();
        string defaultString = string.Empty;

        using var command = accountDbContext.Database.GetDbConnection().CreateCommand();
        command.CommandText = "EXEC MakePayment @Id, @Date, @Description, @Amount, @AccountId";
        command.Parameters.Add(new SqlParameter("@Id", transactionId));
        command.Parameters.Add(new SqlParameter("@Date", transactionDTO.Date));
        command.Parameters.Add(new SqlParameter("@Description", defaultString));
        command.Parameters.Add(new SqlParameter("@Amount", transactionDTO.Amount));
        command.Parameters.Add(new SqlParameter("@AccountId", accountId));

        await accountDbContext.Database.OpenConnectionAsync();
        await command.ExecuteNonQueryAsync();
    }
}
