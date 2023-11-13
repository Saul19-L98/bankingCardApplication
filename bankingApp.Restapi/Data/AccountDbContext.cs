using bankingApp.Restapi.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;

namespace bankingApp.Restapi.Data;

public class AccountDbContext : DbContext
{
    public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options)
    {
    }

    public DbSet<Card> Card { get; set; }
    public DbSet<Account> Account { get; set; }
    public DbSet<TransactionType> TransactionTypes { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //Seed Ids (card,account,TransactionTypePaymment,TransactionTypePurchase,Transaction)
        var cardId = Guid.NewGuid();
        var accountId = Guid.NewGuid();
        var transactionTypePaymmentId = Guid.NewGuid();
        var transactionTypePurchaseId = Guid.NewGuid();
        var transactionPurchasePrevMonth = Guid.NewGuid();
        var transactionPaymentPrevMonth = Guid.NewGuid();
        var transactionId = Guid.NewGuid();

        // Seeding data for Card
        modelBuilder.Entity<Card>().HasData(
            new Card { Id = cardId, Name = "Saúl Laínez", CardNumber = "4550030500456732", ExpirationDate = DateTime.Parse("2028-12-31"), SecurityCode = "123", CreditLimit = 5000 }
        );

        // Seeding data for Account
        modelBuilder.Entity<Account>().HasData(
            new Account 
            { 
                Id = accountId, 
                CardId = cardId, 
                CurrentBalance = 3000, 
                AvailableBalance = 2000,
                BonusInterest = 0.05M,  // 5% bonus interest
                MinimumBalancePercentage = 0.10M  // 10% minimum balance percentage
            }
        );

        // Seeding data for TransactionType
        modelBuilder.Entity<TransactionType>().HasData(
            new TransactionType { Id = transactionTypePaymmentId, Type = "Payment" },
            new TransactionType { Id = transactionTypePurchaseId, Type = "Purchase" }
        );

        // Seeding data for Transaction

        var recentTransactions = new List<Transaction>
        {
            new Transaction
            {
                Id = transactionPurchasePrevMonth,
                AccountId = accountId,
                TransactionTypeId = transactionTypePurchaseId,
                Date = DateTime.Parse("2023-10-02"),
                Amount = 3000.00M,
                Description = "Car reparations."
            },
            new Transaction
            {
                Id = transactionPaymentPrevMonth,
                AccountId = accountId,
                TransactionTypeId = transactionTypePaymmentId,
                Date = DateTime.Parse("2023-10-17"),
                Amount = 3150.00M,
                Description = string.Empty
            },
            new Transaction 
            { 
                Id = transactionId, 
                AccountId = accountId, 
                TransactionTypeId = transactionTypePurchaseId, 
                Date = DateTime.Parse("2023-11-11"), 
                Amount = 3000.00M, 
                Description = "Dell Laptop" 
            }
        };

        modelBuilder.Entity<Transaction>().HasData(recentTransactions);
    }
}
