using bankingApp.Restapi.Models.DTO.TransactionsDTOs;
using bankingApp.Restapi.Repository.TransactionsRepository;
using Microsoft.AspNetCore.Mvc;

namespace bankingApp.Restapi.Controllers.Transactions;

[Route("api/[controller]")]
[ApiController]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionsRepository transactionsRepository;

    public TransactionsController(ITransactionsRepository transactionsRepository)
    {
        this.transactionsRepository = transactionsRepository;
    }
    [HttpPost("purchase")]
    public async Task<IActionResult> AddPurchase([FromBody] PurchaseDTO purchase)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await transactionsRepository.AddPurchase(purchase);
        return Ok("Purchase added successfully.");
    }

    [HttpPost("payment")]
    public async Task<IActionResult> MakePayment([FromBody] PaymentDTO payment)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await transactionsRepository.MakePayment(payment);
        return Ok("Payment made successfully.");
    }
}
