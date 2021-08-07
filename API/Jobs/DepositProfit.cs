#region using
using Quartz;
using System;
using MediatR;
using System.Linq;
using Domain.Model;
using Application.Users;
using System.Diagnostics;
using Application.Helpers;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Application.UserFinancialPackages;
using Application;
#endregion

namespace API.Jobs
{
    [DisallowConcurrentExecution]
    public class DepositProfit : IJob
    {
        #region Fields
        private readonly ILogger<DepositProfit> _logger;
        private readonly IMediator _mediator;
        #endregion

        #region Ctro
        public DepositProfit(ILogger<DepositProfit> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        #endregion

        #region work
        public async Task Execute(IJobExecutionContext context)
        {
            var watch = new Stopwatch();
            watch.Start();

            await CalculateProfitAmountPerDayForEachUser();

            await _mediator.Send(new Save.Command());

            watch.Stop();

            _logger.LogInformation($"Profit from financial packages was deposited in {watch.ElapsedMilliseconds} ms!");
        }
        #endregion

        #region Helper
        /// <summary>
        /// Calculate profit amount per day for each user and itself create a transaction and profit recored in database
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task CalculateProfitAmountPerDayForEachUser()
        {

            var users = await GetAllUsers();

            foreach (var user in users)
            {

                var userFinancialPackages = user.UserFinancialPackages;

                foreach (var UF in userFinancialPackages)
                {
                    if (!IsEndFinancialPackage(UF))
                    {
                        var profitAmountPerDay = UF.ProfitAmountPerDay;

                        await TransactionHelper.CreateTransaction(user, profitAmountPerDay, _mediator);
                        await ProfitHelper.CreateProfit(user, profitAmountPerDay, _mediator);
                    }
                    else
                    {
                        await _mediator.Send(new RemoveUserFinancialPackage.Command(UF));
                    }
                    if (userFinancialPackages.Count is 0)
                        break;
                }
            }

            _logger.LogInformation($"deposit profit for {users.Count()} users");
        }


        /// <summary>
        /// Checks if the user's financial package has expired
        /// </summary>
        /// <param name="UF"></param>
        /// <returns></returns>
        private bool IsEndFinancialPackage(UserFinancialPackage UF) =>
            UF.EndFinancialPackageDate <= DateTime.Now ? true : false;


        /// <summary>
        /// Gives all users that have financial package
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task<IEnumerable<AppUser>> GetAllUsers()
        {
            var users = await _mediator.Send(new GetAllUsersAsync.Query());
            users = users.Where(x => x.UserFinancialPackages.Count() > 0).ToList();
            return users;
        }

        #endregion
    }
}
