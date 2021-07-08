using Persistence.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class RepositoryExtension
    {

        public static void AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IFinancialPackage, FinancialPackageService>();
            services.AddScoped<ITransaction, TransactionService>();
            services.AddScoped<IUser, UserService>();
            services.AddScoped<IUserFinancial, UserFinancialService>();
            services.AddScoped<IProfit, ProfitService>();
        }

    }
}
