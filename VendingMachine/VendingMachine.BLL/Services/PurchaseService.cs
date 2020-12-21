using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendingMachine.BLL.ServiceInterface;
using VendingMachine.DAL.Repositories;
using VendingMachine.DTO;

namespace VendingMachine.BLL.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly PurchaseRepository _repository;
        private readonly IAccountService _accountService;
        public PurchaseService(PurchaseRepository repository,
                               IAccountService accountService)
        {
            this._repository = repository;
            this._accountService = accountService;
        }
        public async Task<Transaction> AddPurchase(Purchase model)
        {
            var transaction = new Transaction();
            
            if (!model.IsCheckOut)
                transaction = await this._repository.AddPurchase(model);

            return transaction;
        }

        public async Task<List<Purchase>> GetAllPurchase(int accountID)
        {
           return await this._repository.GetAllPurchase(accountID);
        }

        public async Task<Purchase> GetByID(int id)
        {
            return await this._repository.GetByID(id);
        }

        public async Task<Transaction> CheckOutItems(List<Purchase> models)
        {
            var transaction = new Transaction();
            var accountId = models.Select(x => x.AccountID).FirstOrDefault();

            var userBalance = await _accountService.GetByID(accountId);

            if (userBalance.Balance > models.Sum(x => x.Amount))
            {
                transaction = await this._repository.CheckOutItems(models);

                if (transaction.IsSuccess)
                {
                    var account = await this._accountService.GetByID(accountId);
                    account.Balance = account.Balance - models.Sum(x => x.Amount);
                    transaction = await this._accountService.UpdateBalance(account);
                }
            }
            else
            {
                transaction.Message = "Insufficient funds";
                transaction.IsSuccess = false;
            }

            return transaction;
        }

        public async Task<List<Purchase>> GetAllSummaryOrder(int accountID)
        {
            return await this._repository.GetAllSummaryOrder(accountID);
        }
    }
}
