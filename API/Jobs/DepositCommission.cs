using Quartz;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence.Repository;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace API.Jobs
{
    [DisallowConcurrentExecution]
    public class DepositCommission : IJob
    {
        private readonly ILogger<DepositCommission> _logger;
        private readonly IUser _user;

        public DepositCommission(
              ILogger<DepositCommission> logger, IUser user)
        {
            _logger = logger;
            _user = user;
        }

        public Task Execute(IJobExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
