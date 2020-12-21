using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VendingMachine.BLL.ServiceInterface;
using VendingMachine.DAL.Repositories;
using VendingMachine.DTO;

namespace VendingMachine.BLL.Services
{
    public class AccountService : IAccountService
    {

        private readonly AccountRepository  _repository;

        public AccountService()
        {
        }

        public AccountService(AccountRepository repository)
        {
            this._repository = repository;
        }
        public async Task<Transaction> AddAccount(Accounts model)
        {
            model.DateCreated = DateTime.UtcNow;

            var transaction = await this._repository.AddAccount(model);
            return transaction;
        }

        public async Task<List<Accounts>> GetAllAccounts()
        {
            return await _repository.GetAllAccounts();
        }

        public async Task<Accounts> GetByID(int id)
        {

            var account = await _repository.GetByID(id);

            
            return account;
        }

        public async Task<Transaction> UpdateBalance(Accounts model)
        {
            return await this._repository.UpdateBalance(model);
        }
    }
}
