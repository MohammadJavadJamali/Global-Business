using Quartz;
using API.Jobs;
using Quartz.Spi;
using Quartz.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class QuartzExtension
    {   
        public static void AddQuatzServices(this IServiceCollection services)
        {
            services.AddSingleton<QuartzJobRunner>();

            services.AddHostedService<QuartzHostedService>();

            services.AddSingleton<IJobFactory, JobFactory>();
            
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();


            services.AddScoped<DepositProfit>();
            services.AddScoped<DepositCommission>();

            //every night at 12:00 PM
            services.AddSingleton(new JobSchedule(jobType: typeof(DepositProfit), cronExpression: "35 54 13 * * ?"));

            //every month at 01 / 00 / 0000 12:00 PM
            services.AddSingleton(new JobSchedule(jobType: typeof(DepositCommission), cronExpression: "50 16 13 * * ?"));


        }

    }
}
