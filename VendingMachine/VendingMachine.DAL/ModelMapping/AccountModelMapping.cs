using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VendingMachine.DTO;

namespace VendingMachine.DAL.ModelMapping
{
    public class AccountModelMapping : IEntityTypeConfiguration<Accounts>
    {
        public void Configure(EntityTypeBuilder<Accounts> builder)
        {
            builder.ToTable("Account");
            builder.HasKey(x => x.ID);
            builder.Property(x => x.ID);
            builder.Property(x => x.GuestID).IsRequired();
            builder.Property(x => x.DateCreated);
        }
    }
}
