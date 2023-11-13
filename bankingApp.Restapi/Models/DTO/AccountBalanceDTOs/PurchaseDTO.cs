namespace bankingApp.Restapi.Models.DTO.AccountBalanceDTOs;

public class PurchaseDTO
{
    public DateTime Date { get; set; }
    public string? Description { get; set; }
    public decimal Amount { get; set; }
}
