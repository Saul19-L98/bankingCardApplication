namespace bankingApp.WebApp.Models.DTOs.AccountBalanceDTOs;

public class PurchaseDTO
{
    public DateTime Date { get; set; }
    public string? Description { get; set; }
    public decimal Amount { get; set; }
}
