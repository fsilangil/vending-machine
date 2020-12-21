using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VendingMachine.DTO;

namespace VendingMachine.DAL.ModelMapping
{
    public class PurchaseModelMapping : IEntityTypeConfiguration<Purchase>
    {
        public void Configure(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("Purchases");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID);
            builder.Property(x => x.AccountID).IsRequired();
            builder.Property(x => x.ProductID).IsRequired();
            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.Quantity).IsRequired();
            builder.Property(x => x.DateCreated);
        }
    }
}
