using Quartz;
using System;
using System.Linq;
using Domain.Model;
using Application.Repository;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Application.Helpers;
using System.Diagnostics;

namespace API.Jobs
{
    [DisallowConcurrentExecution]
    public class DepositProfit : IJob
    {
        #region Fields
        private readonly ISave _save;
        private readonly IUser _user;
        private readonly IProfit _profit;
        private readonly ITransaction _transaction;
        private readonly IUserFinancial _userFinancial;
        private readonly ILogger<DepositProfit> _logger;
        private readonly IFinancialPackage _financialPackage;
        #endregion

        #region Ctro
        public DepositProfit(
              ISave save
            , IUser user
            , IProfit profit
            , ITransaction transaction
            , IUserFinancial userFinancial
            , ILogger<DepositProfit> logger
            , IFinancialPackage financialPackage)
        {
            _save = save;
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

            var users = await GetAllUsers();

            foreach (var user in users)
            {

                var userFinancialPackages = user.UserFinancialPackages;

                foreach (var UF in userFinancialPackages)
                {
                    if (!IsEndFinancialPackage(UF))
                    {
                        //decimal profitAmount = 0;
                        //decimal profitAmountPerDay = 0;

                        //var financialPackage = await GetFinancialPackage(UF);

                        //profitAmount += UF.AmountInPackage * (decimal)financialPackage.ProfitPercent / 100;

                        ////double FinancialPackageDay = GetFinancialPackageDay(UF);

                        //int FinancialPackageDay = UF.DayCount;

                        //profitAmountPerDay += profitAmount / FinancialPackageDay;

                        var profitAmountPerDay = UF.ProfitAmountPerDay;

                        TransactionHelper.CreateTransaction(_user, user, profitAmountPerDay, _transaction);
                        await ProfitHelper.CreateProfit(user, _profit, profitAmountPerDay);

                        _logger.LogInformation($"profit amount per day : {profitAmountPerDay}");

                    }
                    else
                    {
                        await _userFinancial.DeleteAsync(UF);
                        _logger.LogInformation($"delete user financial package");
                    }
                    if (userFinancialPackages.Count is 0)
                        break;
                }
            }

            await _save.SaveChangeAsync();

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
        private async Task<FinancialPackage> GetFinancialPackage(UserFinancialPackage uf) =>
            await _financialPackage
                .FirstOrDefaultAsync(x => x.Id == uf.FinancialPackageId, y => y.UserFinancialPackages);



        /// <summary>
        /// Gives all users that have financial package
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task<IEnumerable<AppUser>> GetAllUsers() =>
            await _user.GetAll(x => x.UserFinancialPackages.Count() > 0, y => y.UserFinancialPackages);


        ///// <summary>
        ///// Calculates the number of days in a financial package
        ///// </summary>
        ///// <param name="UF"></param>
        ///// <returns></returns>
        //private double GetFinancialPackageDay(UserFinancialPackage UF) =>
        //    (UF.EndFinancialPackageDate - UF.ChoicePackageDate).TotalDays;

        #endregion
    }
}
