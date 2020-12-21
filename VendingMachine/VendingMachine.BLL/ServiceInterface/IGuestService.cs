using System.Collections.Generic;
using System.Threading.Tasks;
using VendingMachine.DTO;

namespace VendingMachine.BLL.ServiceInterface
{
    public interface IGuestService
    {
        Task<List<Guest>> GetAllGuests();
        Task<Guest> GetByID(int id);
        Task<Transaction> AddGuest(Guest model, double balance);

        Task<Accounts> IsUserExist(string email);
    }
}
