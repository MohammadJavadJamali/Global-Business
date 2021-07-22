using Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class DataContextExtension
    {
        public static void AddDataContextServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<DataContext>(options => options
                .UseSqlServer(config.GetConnectionString("DefualtConnection")));
        }
    }
}
