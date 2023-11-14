namespace bankingApp.WebApp.Models.DTOs.TransactionsHistoryDTOs;

public class TransactionsHistoryDTO
{
    public DateTime Date { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public string TransactionType { get; set; }
}
