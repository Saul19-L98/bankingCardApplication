using System.ComponentModel.DataAnnotations;

namespace bankingApp.Restapi.Models.DTO.TransactionsDTOs;

public class PurchaseDTO
{
    [Required(ErrorMessage = "Date is required.")]
    public DateTime Date { get; set; }

    [Required(ErrorMessage = "Amount is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
    public decimal Amount { get; set; }

    [Required(ErrorMessage = "Description is required")]
    [StringLength(100,ErrorMessage = "Description needs to be shorter.")]
    public string Description { get; set; } = string.Empty;
}
