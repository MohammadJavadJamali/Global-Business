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
using Application.Transactions;
using Application.Profits;
using Serilog;
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
            var start = DateTime.Now;
            decimal totalDeposit = 0;

            var users = await GetAllUsers();
            List<Transaction> transactions = new();
            List<Profit> profits = new();

            foreach (var user in users)
            {
                var userFinancialPackages = user.UserFinancialPackages;

                foreach (var UF in userFinancialPackages)
                {
                    if (!IsEndFinancialPackage(UF))
                    {
                        var profitAmountPerDay = UF.ProfitAmountPerDay;

                        Transaction transaction = new();
                        transaction.Amount = profitAmountPerDay;
                        transaction.InitialBalance = user.AccountBalance;
                        transaction.FinalBalance = user.AccountBalance + profitAmountPerDay;
                        transaction.EmailTargetAccount = user.Email;
                        transaction.User = user;
                        transaction.User_Id = user.Id;
                        transaction.TransactionDate = DateTime.Now;

                        user.AccountBalance += profitAmountPerDay;

                        Profit profit = new();
                        profit.User = user;
                        profit.User_Id = user.Id;
                        profit.ProfitAmount = profitAmountPerDay;
                        profit.ProfitDepositDate = DateTime.Now;

                        transactions.Add(transaction);
                        profits.Add(profit);

                        totalDeposit += profitAmountPerDay;
                    }
                    else
                    {
                        await _mediator.Send(new RemoveUserFinancialPackage.Command(UF));
                    }
                    if (userFinancialPackages.Count is 0)
                        break;
                }
            }

            await _mediator.Send(new CreateListTransactionAsync.Command(transactions));
            await _mediator.Send(new CreateListProfitAsync.Command(profits));
            await _mediator.Send(new UpdateListUserAsync.Command(users.ToList()));

            Log.Logger
                .ForContext("Count", users.Count())
                .ForContext("TotalDeposit", totalDeposit)
                .ForContext("Status", "Success")
                .ForContext("Start", start)
                .ForContext("End", DateTime.Now)
                .ForContext("Type", SP.Profit)
                .Information("");

            Log.Logger.Information($"deposit profit for {users.Count()} users");
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
