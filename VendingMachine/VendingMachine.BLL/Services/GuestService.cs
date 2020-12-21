using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendingMachine.BLL.ServiceInterface;
using VendingMachine.DAL.Repositories;
using VendingMachine.DTO;

namespace VendingMachine.BLL.Services
{
    public class GuestService : IGuestService
    {
        private readonly GuestRepository _repository;
        private readonly IAccountService _accountService;

        public GuestService(GuestRepository repository,
                            IAccountService accountService)
        {
            this._repository = repository;
            this._accountService = accountService;
        }
        public async Task<Transaction> AddGuest(Guest model, double balance)
        {
            var transaction = new Transaction();
            var guests = await this.GetAllGuests();
            var guest = guests.Where(x => x.EmailAddress == model.EmailAddress).FirstOrDefault();

            if (guest != null)
            {
                transaction.Message = "Email address already exists!";
                transaction.IsSuccess = false;
            }
            else
            {
                if (balance <= 0)
                {
                    return new Transaction { Message = "Money should be greater than 0", IsSuccess = false };
                }
                else
                {
                    model.DateCreated = DateTime.UtcNow;
                    var guestid = await this._repository.AddGuest(model);
                    var account = new Accounts { GuestID = guestid, Balance = balance, DateCreated = DateTime.UtcNow };
                    transaction = await this._accountService.AddAccount(account);
                }
            }

            return transaction;
        }



        public async Task<List<Guest>> GetAllGuests()
        {
            return await this._repository.GetAllGuests();
        }

        public async Task<Guest> GetByID(int id)
        {
            return await this._repository.GetByID(id);
        }

        public async Task<Accounts> IsUserExist(string email)
        {
            var account = new Accounts();

            var users = await this.GetAllGuests();
            var user = users.Where(x => x.EmailAddress == email).FirstOrDefault();

            if (user != null)
            {
                var accounts = await this._accountService.GetAllAccounts();
                account = accounts.Where(x => x.GuestID == user.ID).FirstOrDefault();

            }

            return account;

        }
    }
}
