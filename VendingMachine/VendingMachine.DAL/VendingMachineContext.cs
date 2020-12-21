using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VendingMachine.DAL.ModelMapping;

namespace VendingMachine.DAL
{
    public class VendingMachineContext : DbContext
    {
        public VendingMachineContext(DbContextOptions options)
            :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountModelMapping());
            modelBuilder.ApplyConfiguration(new GuestModelMapping());
            modelBuilder.ApplyConfiguration(new ProductModelMapping());
            modelBuilder.ApplyConfiguration(new PurchaseModelMapping());
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

    }
}
