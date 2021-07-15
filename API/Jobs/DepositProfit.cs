using Quartz;
using System;
using System.Linq;
using Domain.Model;
using Persistence.Repository;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace API.Jobs
{
    [DisallowConcurrentExecution]
    public class DepositProfit : IJob
    {
        #region ctor
        private readonly IUser _user;
        private readonly IProfit _profit;
        private readonly ITransaction _transaction;
        private readonly IUserFinancial _userFinancial;
        private readonly ILogger<DepositProfit> _logger;
        private readonly IFinancialPackage _financialPackage;

        public DepositProfit(
              IUser user
            , IProfit profit
            , ITransaction transaction
            , IUserFinancial userFinancial
            , ILogger<DepositProfit> logger
            , IFinancialPackage financialPackage)
        {
            _user = user;
            _logger = logger;
            _profit = profit;
            _transaction = transaction;
            _userFinancial = userFinancial;
            _financialPackage = financialPackage;
        }
        #endregion

        #region work
        public async Task Execute(IJobExecutionContext context)
        {
            await CalculateProfitAmountPerDayForEachUser();
            _logger.LogInformation("Profit from financial packages was deposited!");
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
                        decimal profitAmount = 0;
                        decimal profitAmountPerDay = 0;

                        var financialPackage = await GetFinancialPackage(UF);

                        profitAmount += UF.AmountInPackage * (decimal)financialPackage.ProfitPercent / 100;

                        double FinancialPackageDay = GetFinancialPackageDay(UF);

                        profitAmountPerDay += profitAmount / (decimal)FinancialPackageDay;

                        await CreateTransaction(user, profitAmountPerDay);
                        await CreateProfit(user, profitAmountPerDay);
                    }
                    else
                    {
                        await _userFinancial.DeleteAsync(UF);

                        //context.UserFinancialPackages.Remove(UF);
                        //context.Users.Update(user);
                        //await context.SaveChangesAsync();
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
        private async Task<FinancialPackage> GetFinancialPackage(UserFinancialPackage uf)
        {
            var financial = await _financialPackage
                .FirstOrDefaultAsync(x => x.Id == uf.FinancialPackageId, y => y.UserFinancialPackages);
            return financial;
        }


        /// <summary>
        /// Gives all users that have financial package
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task<List<AppUser>> GetAllUsers()
        {
            var users = await _user.GetAll(x => x.UserFinancialPackages.Count() > 0, y => y.UserFinancialPackages);
            return users.ToList();
        }


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
        private async Task CreateTransaction(AppUser user, decimal transactionAmount)
        {
            Transaction transaction = new();
            //transaction date set in CreteAsync method
            transaction.User = user;
            transaction.Amount = transactionAmount;
            transaction.EmailTargetAccount = user.Email;
            transaction.InitialBalance = user.AccountBalance;
            transaction.FinalBalance = user.AccountBalance + transactionAmount;

            user.AccountBalance += transactionAmount;

            await _transaction.CreateAsync(transaction);

            _user.UpdateAsync(user);

        }


        /// <summary>
        /// A profit is created for the user 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="profitAmount"></param>
        /// <returns></returns>
        private async Task CreateProfit(AppUser user, decimal profitAmount)
        {
            Profit profit = new();
            //date time set in CreateAsync method
            profit.User = user;
            profit.ProfitAmount = profitAmount;

            await _profit.CreateAsync(profit);
        }
        #endregion
    }
}
