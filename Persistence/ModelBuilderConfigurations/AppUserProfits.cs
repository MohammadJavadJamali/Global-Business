using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.ModelBuilderConfigurations
{
    public class AppUserProfits : IEntityTypeConfiguration<Profit>
    {
        public void Configure(EntityTypeBuilder<Profit> builder)
        {
            builder
                .HasOne(u => u.User)
                .WithMany(p => p.Profits)
                .HasForeignKey(u => u.User_Id);
        }
    }
}
