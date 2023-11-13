namespace bankingApp.Restapi.Models.DTO.AccountBalanceDTOs;

public class CardAccountDetailsDTO
{
    public string Name { get; set; }
    public string CardNumber { get; set; }
    public decimal CreditLimit { get; set; }
    public decimal CurrentBalance { get; set; }
    public decimal AvailableBalance { get; set; }
    public decimal BonifiableInterest { get; set; } // Calculated field
    public decimal MinimumPaymentDue { get; set; } // Calculated field
    public decimal TotalCashAmountPayable { get; set; } // Calculated field
    public decimal TotalCashAmountToPayWithInterest { get; set; } // Calculated field
}
