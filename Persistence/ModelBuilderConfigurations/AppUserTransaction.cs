using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Persistence.ModelBuilderConfigurations
{
    public class AppUserTransaction : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder
               .HasOne(u => u.User)
               .WithMany(t => t.Transactions)
               .HasForeignKey(i => i.User_Id);
        }
    }
}
