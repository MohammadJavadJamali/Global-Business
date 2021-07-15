using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.ModelBuilderConfigurations
{
    public class AppUserFinancialPackage : IEntityTypeConfiguration<UserFinancialPackage>
    {
        public void Configure(EntityTypeBuilder<UserFinancialPackage> builder)
        {
            builder
                .HasKey(k => new { k.UserId, k.FinancialPackageId });

            builder
                .HasOne(u => u.User)
                .WithMany(uf => uf.UserFinancialPackages)
                .HasForeignKey(ui => ui.UserId);

            builder
                .HasOne(f => f.FinancialPackage)
                .WithMany(uf => uf.UserFinancialPackages)
                .HasForeignKey(fi => fi.FinancialPackageId);
        }
    }
}
