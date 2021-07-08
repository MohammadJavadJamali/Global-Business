using System;

namespace API.Jobs
{
    public class JobSchedule
    {
        public Type JobType { get; set; }
        public string CronExpression { get; set; }

        public JobSchedule(string cronExpression, Type jobType)
        {
            CronExpression = cronExpression;
            JobType = jobType;
        }
    }
}