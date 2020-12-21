using System.Collections.Generic;
using System.Threading.Tasks;
using VendingMachine.DTO;

namespace VendingMachine.BLL.ServiceInterface
{
    public interface IProductService
    {
        Task<List<Products>> GetAllProducts();
        Task<Products> GetByID(int id);
        Task<Transaction> AddProduct(Products model);
    }
}
