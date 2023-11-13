namespace bankingApp.Restapi.Models.Domain;

public class Card
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string CardNumber { get; set; }
    public DateTime ExpirationDate { get; set; }
    public string SecurityCode { get; set; }
    public decimal CreditLimit { get; set; }

    // Navigation property for the Account
    public Account Account { get; set; }
}
