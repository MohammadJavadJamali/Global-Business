using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Application.ModelBuilderConfigurations
{
    public class AppUserTransaction : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder
               .HasOne(u => u.User)
               .WithMany(t => t.Transactions)
               .HasForeignKey(i => i.UserId);
        }
    }
}
