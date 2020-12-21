using System.Collections.Generic;
using System.Threading.Tasks;
using VendingMachine.DTO;

namespace VendingMachine.BLL.ServiceInterface
{
    public interface IAccountService
    {
        Task<List<Accounts>> GetAllAccounts();
        Task<Accounts> GetByID(int id);
        Task<Transaction> AddAccount(Accounts model);
        Task<Transaction> UpdateBalance(Accounts model);
    }
}
