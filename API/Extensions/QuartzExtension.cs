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
            //services.AddScoped<DepositCommission>();


            services.AddSingleton(new JobSchedule(jobType: typeof(DepositProfit), cronExpression: "0 0/2 * * * ?"));
            //services.AddSingleton(new JobSchedule(jobType: typeof(DepositCommission), cronExpression: "0/40 * * * * ?"));


        }

    }
}
