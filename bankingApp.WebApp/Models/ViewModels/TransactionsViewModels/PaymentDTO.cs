using System.ComponentModel.DataAnnotations;

namespace bankingApp.WebApp.Models.ViewModels.TransactionsViewModels;

public class PaymentDTO
{
    [Required]
    public DateTime Date { get; set; }

    [Required]
    public decimal Amount { get; set; }
}
