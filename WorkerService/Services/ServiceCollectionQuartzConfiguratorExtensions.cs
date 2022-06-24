using Quartz;

namespace WorkerService.Services;

public static class ServiceCollectionQuartzConfiguratorExtensions
{
    public static void AddJobAndTrigger<T>(
        this IServiceCollectionQuartzConfigurator quartz,
        IConfiguration config, string identity = "")
        where T : IJob
    {
        string jobName = typeof(T).Name;

        var configKey = $"Quartz:{jobName}" + identity;
        var cronSchedule = config[configKey];

        if (string.IsNullOrEmpty(cronSchedule))
        {
            throw new Exception($"No Quartz.NET Cron schedule found for job in configuration at {configKey}");
        }

        var jobKey = new JobKey(jobName);
        quartz.AddJob<T>(opts => opts.WithIdentity(jobKey + identity));

        quartz.AddTrigger(opts => opts
            .ForJob(jobKey + identity)
            .WithIdentity(jobName + "-trigger" + identity)
            .WithCronSchedule(cronSchedule));
    }
}
