using bankingApp.Restapi.Data;
using bankingApp.Restapi.Models.DTO.AccountBalanceDTOs;
using Microsoft.EntityFrameworkCore;

namespace bankingApp.Restapi.Repository.AccountBalanceRepository;

public class AccountBalanceRepository : IAccountBalanceRepository
{
    private readonly AccountDbContext accountDbContext;

    public AccountBalanceRepository(AccountDbContext accountDbContext)
    {
        this.accountDbContext = accountDbContext;
    }

    public async Task<CardAccountDetailsDTO?> GetCardAccountDetails()
    {
        using var command = accountDbContext.Database.GetDbConnection().CreateCommand();
        command.CommandText = "EXEC GetCardAccountDetails";
        accountDbContext.Database.OpenConnection();

        using var result = await command.ExecuteReaderAsync();
        if (await result.ReadAsync())
        {
            return new CardAccountDetailsDTO
            {
                Name = result.GetString(result.GetOrdinal("Name")),
                CardNumber = result.GetString(result.GetOrdinal("CardNumber")),
                CreditLimit = result.GetDecimal(result.GetOrdinal("CreditLimit")),
                CurrentBalance = result.GetDecimal(result.GetOrdinal("CurrentBalance")),
                AvailableBalance = result.GetDecimal(result.GetOrdinal("AvailableBalance")),
                BonifiableInterest = result.GetDecimal(result.GetOrdinal("BonifiableInterest")),
                MinimumPaymentDue = result.GetDecimal(result.GetOrdinal("MinimumPaymentDue")),
                TotalCashAmountPayable = result.GetDecimal(result.GetOrdinal("TotalCashAmountPayable")),
                TotalCashAmountToPayWithInterest = result.GetDecimal(result.GetOrdinal("TotalCashAmountToPayWithInterest"))
            };
        }

        return null;
    }
    public async Task<List<PurchaseDTO>?> GetPurchasesForCurrentMonth()
    {
        var purchases = new List<PurchaseDTO>();

        using (System.Data.Common.DbCommand command = accountDbContext.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = "EXEC GetPurchasesForCurrentMonth";
            accountDbContext.Database.OpenConnection();

            using var result = await command.ExecuteReaderAsync();
            while (await result.ReadAsync())
            {
                purchases.Add(new PurchaseDTO
                {
                    Date = result.GetDateTime(result.GetOrdinal("Date")),
                    Description = result.GetString(result.GetOrdinal("Description")),
                    Amount = result.GetDecimal(result.GetOrdinal("Amount"))
                });
            }
        }
        if (purchases != null)
        {
            return purchases;
        }
        return null;
    }
    public async Task<TotalPurchasesCurrentAndPreviousMonthDTO> GetTotalPurchasesCurrentAndPreviousMonth()
    {
        var totals = new TotalPurchasesCurrentAndPreviousMonthDTO();
        using (System.Data.Common.DbCommand command = accountDbContext.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = "EXEC GetTotalPurchasesCurrentAndPreviousMonth";
            accountDbContext.Database.OpenConnection();

            using var result = await command.ExecuteReaderAsync();
            // Read total for the current month
            if (await result.ReadAsync())
            {
                totals.TotalCurrentMonth = result.IsDBNull(result.GetOrdinal("TotalCurrentMonth"))
                    ? 0
                    : result.GetDecimal(result.GetOrdinal("TotalCurrentMonth"));
            }

            // Move to the next result set (total for the previous month)
            if (await result.NextResultAsync())
            {
                if (await result.ReadAsync())
                {
                    totals.TotalPreviousMonth = result.IsDBNull(result.GetOrdinal("TotalPreviousMonth"))
                        ? 0
                        : result.GetDecimal(result.GetOrdinal("TotalPreviousMonth"));
                }
            }
        }

        return totals;
    }
}
