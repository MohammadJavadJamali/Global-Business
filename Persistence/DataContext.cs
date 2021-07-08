using Domain.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { 
        }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<FinancialPackage> FinancialPackages { get; set; }

        public DbSet<UserFinancialPackage> UserFinancialPackages { get; set; }

        public DbSet<Profit> Profits { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //add relation between User and Transaction (one to many)
            modelBuilder.Entity<Transaction>()
                .HasOne(u => u.User)
                .WithMany(t => t.Transactions)
                .HasForeignKey(i => i.User_Id);


            ////relation between user and profit (one to many)
            modelBuilder.Entity<Profit>()
                .HasOne(u => u.User)
                .WithMany(p => p.Profits)
                .HasForeignKey(u => u.User_Id);


            modelBuilder.Entity<IdentityRole>()
                .HasData(
                    new IdentityRole
                    {
                        Name = "Admin",
                        NormalizedName = "ADMIN"
                    },
                    new IdentityRole
                    {
                        Name = "Customer",
                        NormalizedName = "CUSTOMER"
                    }
                );
            ;
        }
    }
}
