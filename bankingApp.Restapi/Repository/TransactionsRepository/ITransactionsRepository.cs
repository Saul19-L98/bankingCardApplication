using bankingApp.Restapi.Models.DTO.TransactionsDTOs;

namespace bankingApp.Restapi.Repository.TransactionsRepository;

public interface ITransactionsRepository
{
    Task AddPurchase(PurchaseDTO purchaseDTO);
    Task MakePayment(PaymentDTO paymentDTO);
}
