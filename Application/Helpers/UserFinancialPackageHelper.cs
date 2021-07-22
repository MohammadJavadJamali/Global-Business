using System;
using Domain.Model;
using Application.Repository;
using System.Threading.Tasks;
using Domain.DTO.FinancialDTO;

namespace Application.Helpers
{
    public static class UserFinancialPackageHelper
    {
        /// <summary>
        /// AppUser should include UserFinancialPackage
        /// </summary>
        /// <param name="user"></param>
        /// <param name="_userFinancial"></param>
        /// <param name="userFinancialDTO"></param>
        /// <param name="_financialPackage"></param>
        /// <returns></returns>
        public static async Task<bool> CreateUserFinancialPackage(
              AppUser user
            , IUserFinancial _userFinancial
            , UserFinancialDTO userFinancialDTO
            , IFinancialPackage _financialPackage)
        {
            var financialPackageFromDb = await _financialPackage.GetByIdAsync(userFinancialDTO.FinancialPackageId);

            if (financialPackageFromDb is null)
                return false;


            if (DontHaveEnoughMony(user, userFinancialDTO, _userFinancial))
                return false;

            var userFinance = new UserFinancialPackage();

            //Dates are set in the create function in the repository
            userFinance.User = user;
            userFinance.FinancialPackage = financialPackageFromDb;
            userFinance.AmountInPackage = userFinancialDTO.AmountInPackage;

            try
            {
                await _userFinancial.CreateAsync(userFinance);
            }
            catch (Exception)
            {

                throw;
            }


            decimal profitAmountPerDay = 0;

            foreach (var UF in user.UserFinancialPackages)
            {
                decimal profitAmount = 0;

                var financialPackage = await GetFinancialPackage(userFinance, _financialPackage);

                profitAmount += UF.AmountInPackage * (decimal)financialPackage.ProfitPercent / 100;

                int FinancialPackageDay = UF.DayCount;

                profitAmountPerDay += profitAmount / FinancialPackageDay;
            }

            userFinance.ProfitAmountPerDay += profitAmountPerDay;

            try
            {

                await _userFinancial.UpdateAsync(userFinance);
                return true;
            }
            catch(Exception)
            {
                throw;
            }

        }




        /// <summary>
        /// Gives financial packages for each user
        /// </summary>
        /// <param name="context"></param>
        /// <param name="UF"></param>
        /// <returns></returns>
        private static async Task<FinancialPackage> GetFinancialPackage(
              UserFinancialPackage uf
            , IFinancialPackage _financialPackage) =>

                await _financialPackage
                    .FirstOrDefaultAsync(x => x.Id == uf.FinancialPackageId, y => y.UserFinancialPackages);


        private static bool DontHaveEnoughMony(
              AppUser user
            , UserFinancialDTO financialDTO
            , IUserFinancial _userFinancial)
        {
            decimal sumAmountInPackages = SumAmountInPackages(user, _userFinancial);

            if (sumAmountInPackages == 0)
            {
                return user.AccountBalance < financialDTO.AmountInPackage ? true : false;
            }

            return user.AccountBalance < sumAmountInPackages + financialDTO.AmountInPackage ? true : false;
        }

        private static decimal SumAmountInPackages(AppUser user, IUserFinancial _userFinancial)
        {
            decimal sumAmountInPackages = 0;

            var userFinancials = _userFinancial
                .Where(x => x.UserId == user.Id);

            foreach (var finance in userFinancials)
            {
                sumAmountInPackages += finance.AmountInPackage;
            }
            return sumAmountInPackages;
        }

    }
}
