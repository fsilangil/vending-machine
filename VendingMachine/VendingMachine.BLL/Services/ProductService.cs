using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendingMachine.BLL.ServiceInterface;
using VendingMachine.DAL.Repositories;
using VendingMachine.DTO;

namespace VendingMachine.BLL.Services
{
    
    public class ProductService : IProductService
    {
        private readonly ProductRepository _repository;
        public ProductService(ProductRepository repository)
        {
            this._repository = repository;
        }

        public async Task<Transaction> AddProduct(Products model)
        {
            var transaction = new Transaction();
            model.DateCreated = DateTime.Now;
            var products = await this.GetAllProducts();

            var product = products.Where(x => x.Name == model.Name).FirstOrDefault();

            if (product != null)
            {
                product.Quantity = product.Quantity + model.Quantity;
                product.DateCreated = DateTime.UtcNow;
                transaction = await this._repository.AddQuantity(product);
            }
            else
            {
                transaction = await this._repository.AddProduct(model);
            }
                
            return transaction;
        }

        public async Task<List<Products>> GetAllProducts()
        {
            return await _repository.GetAllProducts();
        }

        public async Task<Products> GetByID(int id)
        {
            return await _repository.GetByID(id);
        }
    }
}
