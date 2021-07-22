using Domain.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Application.ModelBuilderConfigurations;

namespace Application
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Node> Nodes { get; set; }

        public DbSet<Profit> Profits { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<FinancialPackage> FinancialPackages { get; set; }

        public DbSet<UserFinancialPackage> UserFinancialPackages { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new AppUserNode());

            modelBuilder.ApplyConfiguration(new AppUserFinancialPackage());

            modelBuilder.ApplyConfiguration(new AppUserTransaction());

            modelBuilder.ApplyConfiguration(new AppUserProfits());

            modelBuilder.ApplyConfiguration(new RoolSeed());
                
        }
    }
}
