namespace bankingApp.Restapi.Models.Domain;

public class TransactionType
{
    public Guid Id { get; set; }
    public string Type { get; set; } // Could be an enum (Purchase, Payment)

    // Navigation property
    public ICollection<Transaction> Transactions { get; set; }
}
