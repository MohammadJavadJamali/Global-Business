using Quartz;
using System;
using Persistence;
using System.Linq;
using Domain.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace API.Jobs
{
    [DisallowConcurrentExecution]
    public class DepositProfit : IJob
    {
        #region ctor
        private readonly IConfiguration _config;
        private readonly ILogger<DepositProfit> _logger;
        public DepositProfit(IConfiguration config, ILogger<DepositProfit> logger)
        {
            _config = config;
            _logger = logger;
        }
        #endregion

        #region work
        public async Task Execute(IJobExecutionContext context)
        {
            var option = new DbContextOptionsBuilder<DataContext>();
            option.UseSqlServer(_config.GetConnectionString("DefualtConnection"));

            using (DataContext dataContext = new DataContext(option.Options))
            {
                await CalculateProfitAmountPerDayForEachUser(dataContext);
                _logger.LogInformation("Profit from financial packages was deposited!");
            }
        }
        #endregion

        #region Helper
        /// <summary>
        /// Calculate profit amount per day for each user and itself create a transaction and profit recored in database
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task CalculateProfitAmountPerDayForEachUser(DataContext context)
        {

            var users = await GetAllUsers(context);

            foreach (var user in users)
            {

                var userFinancialPackages = user.UserFinancialPackages;

                foreach (var UF in userFinancialPackages)
                {
                    if (!IsEndFinancialPackage(UF))
                    {
                        decimal profitAmount = 0;
                        decimal profitAmountPerDay = 0;

                        var financialPackage = await GetFinancialPackage(context, UF);

                        profitAmount += UF.AmountInPackage * (decimal)financialPackage.ProfitPercent / 100;

                        double FinancialPackageDay = GetFinancialPackageDay(UF);

                        profitAmountPerDay += profitAmount / (decimal)FinancialPackageDay;

                        await CreateTransaction(user, profitAmountPerDay, context);
                        await CreateProfit(user, profitAmountPerDay, context);
                    }
                    else
                    {
                        context.UserFinancialPackages.Remove(UF);
                        context.Users.Update(user);
                        await context.SaveChangesAsync();
                    }
                }

            }
        }


        /// <summary>
        /// Checks if the user's financial package has expired
        /// </summary>
        /// <param name="UF"></param>
        /// <returns></returns>
        private bool IsEndFinancialPackage(UserFinancialPackage UF) =>
            UF.EndFinancialPackageDate <= DateTime.Now ? true : false;


        /// <summary>
        /// Gives financial packages for each user
        /// </summary>
        /// <param name="context"></param>
        /// <param name="UF"></param>
        /// <returns></returns>
        private async Task<FinancialPackage> GetFinancialPackage(DataContext context, UserFinancialPackage UF) =>
            await context
                .FinancialPackages
                .Include(f => f.UserFinancialPackages)
                .FirstOrDefaultAsync(x => x.Id == UF.FinancialPackageId);


        /// <summary>
        /// Gives all users that have financial package
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task<List<AppUser>> GetAllUsers(DataContext context) =>
            await context
                .Users
                .Include(u => u.UserFinancialPackages)
                .Where(f => f.UserFinancialPackages.Count() > 0)
                .ToListAsync();

        /// <summary>
        /// Calculates the number of days in a financial package
        /// </summary>
        /// <param name="UF"></param>
        /// <returns></returns>
        private double GetFinancialPackageDay(UserFinancialPackage UF) =>
            (UF.EndFinancialPackageDate - UF.ChoicePackageDate).TotalDays;


        /// <summary>
        /// Creates a transaction to pay profit
        /// </summary>
        /// <param name="user"></param>
        /// <param name="transactionAmount"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task CreateTransaction(AppUser user, decimal transactionAmount, DataContext context)
        {
            Transaction transaction = new();

            transaction.User = user;
            transaction.Amount = transactionAmount;
            transaction.TransactionDate = DateTime.Now;
            transaction.EmailTargetAccount = user.Email;
            transaction.InitialBalance = user.AccountBalance;
            transaction.FinalBalance = user.AccountBalance + transactionAmount;

            user.AccountBalance += transactionAmount;

            context.Transactions.Add(transaction);
            context.Users.Update(user);

            await context.SaveChangesAsync();
        }


        /// <summary>
        /// A profit is created for the user 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="profitAmount"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task CreateProfit(AppUser user, decimal profitAmount, DataContext context)
        {
            Profit profit = new();
            profit.ProfitAmount = profitAmount;
            profit.User = user;
            profit.ProfitDepositDate = DateTime.Now;

            context.Profits.Add(profit);
            
            user.Profits.Add(profit);

            await context.SaveChangesAsync();
        }
        #endregion
    }
}
