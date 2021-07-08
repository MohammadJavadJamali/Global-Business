using System;
using Quartz;
using Quartz.Spi;
using Microsoft.Extensions.DependencyInjection;

namespace API.Jobs
{
    public class SingletonJobFactory : IJobFactory
    {
        #region constructor and fields

        private readonly IServiceProvider _serviceProvider;
        public SingletonJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        #endregion

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return _serviceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob;
        }

        public void ReturnJob(IJob job)
        {
        }
    }
}
