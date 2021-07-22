using Quartz;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

public class QuartzJobRunner : IJob
{
    #region Fields
    private readonly IServiceProvider _serviceProvider;
    #endregion

    #region Ctro
    public QuartzJobRunner(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    #endregion

    public async Task Execute(IJobExecutionContext context)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var jobType = context.JobDetail.JobType;
            var job = scope.ServiceProvider.GetRequiredService(jobType) as IJob;

            await job.Execute(context);
        }
    }
}
