using Quartz;
using WorkerService.Jobs;
using WorkerService.Services;

namespace WorkerService
{
    public static class QuartzExtension
    {   
        public static void AddQuatzServices(this IServiceCollection services, IConfiguration conf)
        {
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();

                q.AddJobAndTrigger<DepositCommission>(conf);
                q.AddJobAndTrigger<DepositProfit>(conf);
            });

            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        }

    }
}
