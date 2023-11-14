namespace bankingApp.WebApp.Models.DTOs.AccountBalanceDTOs;

public class CardAccountDetailsDTO
{
    public string Name { get; set; }
    public string CardNumber { get; set; }
    public decimal CreditLimit { get; set; }
    public decimal CurrentBalance { get; set; }
    public decimal AvailableBalance { get; set; }
    public decimal BonifiableInterest { get; set; }
    public decimal MinimumPaymentDue { get; set; }
    public decimal TotalCashAmountPayable { get; set; } 
    public decimal TotalCashAmountToPayWithInterest { get; set; } 
}
