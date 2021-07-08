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
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            services.AddSingleton<DepositProfit>();
            //0 0 12 * * ?          0/10 * * * * ?
            services.AddSingleton(new JobSchedule(jobType: typeof(DepositProfit), cronExpression: "0 0/10 * * * ?"));

            services.AddHostedService<QuartzHostedService>();

        }

    }
}
