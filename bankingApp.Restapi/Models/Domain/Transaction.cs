namespace bankingApp.Restapi.Models.Domain;

public class Transaction
{
    public Guid Id { get; set; }
    public Guid AccountId { get; set; }
    public Guid TransactionTypeId { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string Description { get; set; }

    // Navigation properties
    public Account Account { get; set; }
    public TransactionType TransactionType { get; set; }
}
