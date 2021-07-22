using Application.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class RepositoryExtension
    {

        public static void AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<ISave, Save>();
            services.AddScoped<INode, NodeService>();
            services.AddScoped<IUser, UserService>();
            services.AddScoped<IProfit, ProfitService>();
            services.AddScoped<ITransaction, TransactionService>();
            services.AddScoped<IUserFinancial, UserFinancialService>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IFinancialPackage, FinancialPackageService>();
        }

    }
}
