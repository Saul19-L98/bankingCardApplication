namespace bankingApp.Restapi.Models.Domain;

public class Account
{
    public Guid Id { get; set; }
    public Guid CardId { get; set; }
    public decimal CurrentBalance { get; set; }
    public decimal AvailableBalance { get; set; } 
    public decimal BonusInterest { get; set; } // 5% interest rate
    public decimal MinimumBalancePercentage { get; set; }// 10% minimum balance percentage
    public decimal BonifiableInterest { get; set; } // Calculated field
    public decimal MinimumPaymentDue { get; set; } // Calculated field
    public decimal TotalCashAmountPayable { get; set; } // Calculated field
    public decimal TotalCashAmountToPayWithInterest { get; set; } // Calculated field


    // Navigation properties
    public Card Card { get; set; }
    public ICollection<Transaction> Transactions { get; set; }

    // Method to update all calculated fields
    public void UpdateCalculatedFields()
    {
        BonifiableInterest = CurrentBalance * BonusInterest;
        MinimumPaymentDue = CurrentBalance * MinimumBalancePercentage;
        TotalCashAmountPayable = CurrentBalance;
        TotalCashAmountToPayWithInterest = CurrentBalance + BonifiableInterest;
    }
}
