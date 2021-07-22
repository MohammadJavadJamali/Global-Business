using System;

namespace API.Jobs
{
    public class JobSchedule
    {
        #region Properties
        public Type JobType { get; set; }
        public string CronExpression { get; set; }

        #endregion

        #region Ctro
        public JobSchedule(string cronExpression, Type jobType)
        {
            CronExpression = cronExpression;
            JobType = jobType;
        }
        #endregion
    }
}