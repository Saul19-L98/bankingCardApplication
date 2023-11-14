using System.ComponentModel.DataAnnotations;

namespace bankingApp.WebApp.Models.ViewModels.PaymentViewModels;

public class PaymentDTO
{
    [Required]
    public DateTime Date { get; set; }

    [Required]
    public decimal Amount { get; set; }
}
