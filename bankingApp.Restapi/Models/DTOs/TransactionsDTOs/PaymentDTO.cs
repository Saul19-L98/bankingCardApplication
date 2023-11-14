using System.ComponentModel.DataAnnotations;

namespace bankingApp.Restapi.Models.DTO.TransactionsDTOs;

public class PaymentDTO
{
    [Required(ErrorMessage = "Date is required.")]
    public DateTime Date { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
    public decimal Amount { get; set; }
}
