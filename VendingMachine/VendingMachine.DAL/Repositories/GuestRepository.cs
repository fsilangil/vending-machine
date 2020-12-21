using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VendingMachine.DTO;

namespace VendingMachine.DAL.Repositories
{
    public class GuestRepository
    {
        private DbSet<Guest> _entities;
        private readonly VendingMachineContext _context;

        protected DbSet<Guest> Entities
        {
            get
            {
                if (this._entities == null)
                    this._entities = this._context.Set<Guest>();

                return this._entities;
            }
        }

        public GuestRepository(VendingMachineContext context)
        {
            this._context = context;
        }
        public async Task<int> AddGuest(Guest model)
        {
            int id = 0;

            try
            {
                this.Entities.Add(model);
                await this._context.SaveChangesAsync();
                id = model.ID;                
            }
            catch (Exception)
            {
                id = 0;
            }

            return id;
        }

        public async Task<List<Guest>> GetAllGuests()
        {
            return await this.Entities.ToListAsync();
        }

        public async Task<Guest> GetByID(int id)
        {
            return await this.Entities.FindAsync(id);
        }
    }
}
