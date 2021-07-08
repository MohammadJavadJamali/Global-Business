using Persistence;
using System.Text;
using API.Services;
using Domain.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace API.Extensions
{
    public static class IdentityServiceExtension
    {

        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {

            var builder = services.AddIdentityCore<AppUser>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
            });
            builder.AddRoles<IdentityRole>();
            builder.AddSignInManager<SignInManager<AppUser>>();
            builder.AddEntityFrameworkStores<DataContext>();



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddScoped<IAuthenticationManager, AuthenticationManager>();
            return services;
        }

    }
}
