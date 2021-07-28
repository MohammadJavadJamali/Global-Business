using MediatR;
using API.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance;
using Microsoft.EntityFrameworkCore;
using Application.Users;
using System.Data;
using Microsoft.Data.SqlClient;

namespace API
{
    public class Startup
    {
        #region ctor and properties

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            string dbConnectionString = Configuration.GetConnectionString("DefaultConnection");

            services.AddTransient<IDbConnection>((sp) => new SqlConnection(dbConnectionString));

            services.AddControllers();

            services.AddSwaggerGen();

            services.AddQuatzServices();
            
            services.AddIdentityServices(Configuration);

            services.AddMediatR(typeof(UpdateUserAsync).Assembly);

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }
            
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
