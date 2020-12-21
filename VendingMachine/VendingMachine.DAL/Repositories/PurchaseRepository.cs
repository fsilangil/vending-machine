using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using VendingMachine.DTO;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;

namespace VendingMachine.DAL.Repositories
{
    public class PurchaseRepository 
    {
        private DbSet<Purchase> _entities;
        private readonly VendingMachineContext _context;

        protected DbSet<Purchase> Entities
        {
            get
            {
                if (this._entities == null)
                    this._entities = this._context.Set<Purchase>();

                return this._entities;
            }
        }

        public PurchaseRepository(VendingMachineContext context)
        {
            this._context = context;
        }
        public async Task<Transaction> AddPurchase(Purchase model)
        {
            try
            {
                this.Entities.Add(model);
                await this._context.SaveChangesAsync();
                return new Transaction { Message = "Purchase Succesful!", IsSuccess = true };
            }
            catch (Exception)
            {
                return new Transaction { Message = "Error Purchase", IsSuccess = false };
            }
        }

        public async Task<List<Purchase>> GetAllPurchase(int accountID)
        {
            return await this.Entities.Where(x => x.AccountID == accountID && !x.IsCheckOut).ToListAsync();
        }

        public async Task<List<Purchase>> GetAllSummaryOrder(int accountID)
        {
            return await this.Entities.Where(x => x.AccountID == accountID && x.IsCheckOut).ToListAsync();
        }
        public async Task<Transaction> CheckOutItems(List<Purchase> models)
        {
            var purchase = await this.GetAllPurchase(models.Select(x => x.AccountID).FirstOrDefault());
            purchase.ForEach(p => p.IsCheckOut = true);
            await this._context.SaveChangesAsync();

            var transaction = new Transaction { Message = "Checkout successful!", IsSuccess = true };

            return transaction;
        }
        public async Task<Purchase> GetByID(int id)
        {
            return await this.Entities.FindAsync(id);
        }

    }
}
