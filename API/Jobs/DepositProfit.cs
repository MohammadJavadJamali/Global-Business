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
            bool done = false;
            DateTime start = DateTime.Now;

            do
            {
                var watch = new Stopwatch();
                watch.Start();

                var users = await GetAllUsers();

                var totalProfit = await CalculateProfitAmountPerDayForEachUser(users.ToList());

                #region profit
                var profitResult = await _mediator.Send(new CreateListProfitAsync.Command(profits));
                if (profitResult < 0)
                {
                    WriteLog(0, totalProfit, SP.Fail, start, DateTime.Now, SP.Profit
                        , "problem in create profits");

                    continue;
                }
                #endregion

                #region transaction
                var transactionResult = await _mediator.Send(new CreateListTransactionAsync.Command(transactions));
                if (transactionResult < 0)
                {
                    WriteLog(0, totalProfit, SP.Fail, start, DateTime.Now, SP.Profit
                        , "problem in create transactions");
                    continue;
                }
                #endregion

                #region user
                var userResult = await _mediator.Send(new UpdateListUserAsync.Command(users.ToList()));
                if (userResult < 0)
                {
                    WriteLog(0, totalProfit, SP.Fail, start, DateTime.Now, SP.Profit
                         , "problem in update users");

                    continue;
                }
                #endregion

                watch.Stop();

                WriteLog(users.Count(), totalProfit, SP.Success, start, DateTime.Now, SP.Profit
                         , $"profits were paid in {watch.ElapsedMilliseconds} ms!");

                done = true;
            } while (!done);

        }
        #endregion

        #region Helper

        List<Transaction> transactions = new();
        IQueryable<Profit> profits ;

        /// <summary>
        /// Calculate profit amount per day for each user and itself create a transaction and profit recored in database
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task<decimal> CalculateProfitAmountPerDayForEachUser(List<AppUser> users)
        {
            decimal totalDeposit = 0;

            foreach (var user in users)
            {
                var userFinancialPackages = user.UserFinancialPackages;

                foreach (var UF in userFinancialPackages)
                {
                    if (!IsEndFinancialPackage(UF))
                    {
                        var profitAmountPerDay = UF.ProfitAmountPerDay;

                        #region transaction
                        Transaction transaction = new();
                        transaction.Amount = profitAmountPerDay;
                        transaction.InitialBalance = user.AccountBalance;
                        transaction.FinalBalance = user.AccountBalance + profitAmountPerDay;
                        transaction.EmailTargetAccount = user.Email;
                        transaction.User = user;
                        transaction.User_Id = user.Id;
                        transaction.TransactionDate = DateTime.Now;
                        #endregion

                        user.AccountBalance += profitAmountPerDay;

                        #region profit
                        Profit profit = new();
                        profit.User = user;
                        profit.User_Id = user.Id;
                        profit.ProfitAmount = profitAmountPerDay;
                        profit.ProfitDepositDate = DateTime.Now;
                        #endregion

                        transactions.Add(transaction);
                        profits.Append(profit);

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
            return totalDeposit;
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

        private void WriteLog(int count, decimal totalCommision, string status, DateTime start,
            DateTime end, string type, string message)
        {
            Log.Logger
                .ForContext("Count", count)
                .ForContext("TotalDeposit", totalCommision)
                .ForContext("Status", status)
                .ForContext("Start", start)
                .ForContext("End", end)
                .ForContext("Type", type)
                .Warning(message);
        }
        #endregion
    }
}
