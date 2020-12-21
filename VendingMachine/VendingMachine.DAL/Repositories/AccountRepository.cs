using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VendingMachine.DTO;

namespace VendingMachine.DAL.Repositories
{
    public class AccountRepository
    {
        private DbSet<Accounts> _entities;
        private readonly VendingMachineContext _context;

        protected DbSet<Accounts> Entities
        {
            get
            {
                if (this._entities == null)
                    this._entities = this._context.Set<Accounts>();

                return this._entities;
            }
        }

        public AccountRepository(VendingMachineContext context)
        {
            this._context = context;
        }

        public async Task<Transaction> AddAccount(Accounts model)
        {
            try
            {
                this.Entities.Add(model);
                await this._context.SaveChangesAsync();
                return new Transaction { Message = "Account created", IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new Transaction { Message = "Error Account", IsSuccess = false };
            }
        }

        public async Task<List<Accounts>> GetAllAccounts()
        {
            return await this.Entities.ToListAsync();
        }

        public async Task<Accounts> GetByID(int id)
        {
            return await this.Entities.FindAsync(id);
        }

        public async Task<Transaction> UpdateBalance(Accounts model)
        {
            this._context.Entry(model).State = EntityState.Modified;
            await this._context.SaveChangesAsync();

            var transaction = new Transaction { Message = "Balance updated", IsSuccess = true };

            return transaction;
        }
    }
}
