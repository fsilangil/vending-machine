using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VendingMachine.DTO;

namespace VendingMachine.DAL.Repositories
{
    public class ProductRepository
    {
        private DbSet<Products> _entities;
        private readonly VendingMachineContext _context;

        protected DbSet<Products> Entities
        {
            get
            {
                if (this._entities == null)
                    this._entities = this._context.Set<Products>();

                return this._entities;
            }
        }
        public ProductRepository(VendingMachineContext context)
        {
            this._context = context;
        }
        public async Task<Transaction> AddProduct(Products model)
        {
            try
            {
                this.Entities.Add(model);
                await this._context.SaveChangesAsync();
                return new Transaction { Message = "Product Created!", IsSuccess = true };
            }
            catch (Exception)
            {
                return new Transaction { Message = "Error Product", IsSuccess = false };
            }
        }

        public async Task<List<Products>> GetAllProducts()
        {
            return await this.Entities.ToListAsync();
        }

        public async Task<Products> GetByID(int id)
        {
            return await this.Entities.FindAsync(id);
        }

        public async Task<Transaction> AddQuantity(Products model)
        {
            this._context.Entry(model).State = EntityState.Modified;
            await this._context.SaveChangesAsync();

            var transaction = new Transaction { Message = "Quantity succesfully added", IsSuccess = true };

            return transaction;
        }
    }
}
