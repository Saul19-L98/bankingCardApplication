using System.ComponentModel.DataAnnotations;

namespace bankingApp.WebApp.Models.ViewModels.PurchaseViewModels;

public class PurchaseDTO
{
    [Required]
    public DateTime Date { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
    public decimal Amount { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "Description needs to be shorter.")]
    public string Description { get; set; } = string.Empty;
}
