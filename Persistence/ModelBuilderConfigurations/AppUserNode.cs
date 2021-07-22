using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.ModelBuilderConfigurations
{
    public class AppUserNode : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {

            builder
                .HasOne(u => u.Node)
                .WithOne(u => u.AppUser)
                .HasForeignKey<Node>(u => u.UserId);

        }
    }
}
