using System.Collections.Generic;
using System.Threading.Tasks;
using VendingMachine.DTO;

namespace VendingMachine.BLL.ServiceInterface
{
    public interface IPurchaseService
    {
        Task<List<Purchase>> GetAllPurchase(int accountID);
        Task<List<Purchase>> GetAllSummaryOrder(int accountID);
        Task<Purchase> GetByID(int id);
        Task<Transaction> AddPurchase(Purchase model);
        Task<Transaction> CheckOutItems(List<Purchase> models);

    }
}
