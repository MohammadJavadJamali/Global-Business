using System;
using MediatR;
using System.Linq;
using Domain.Model;
using Application.Nodes;
using System.Threading.Tasks;
using Domain.DTO.FinancialDTO;
using Application.FinancialPackages;
using Application.UserFinancialPackages;

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
            , UserFinancialDTO userFinancialDTO
            , IMediator mediator)
        {
            var financialPackage = await mediator.Send(new FindFinancialPackageByIdAsync.Query(userFinancialDTO.FinancialPackageId));

            if (financialPackage is null)
                return false;


            if (await DontHaveEnoughMony(user, userFinancialDTO, mediator))
                return false;

            var userFinance = new UserFinancialPackage();

            //Dates are set in the create function in the repository
            userFinance.User = user;
            userFinance.FinancialPackage = financialPackage;
            userFinance.AmountInPackage = userFinancialDTO.AmountInPackage;
            userFinance.ChoicePackageDate = DateTime.Now;
            userFinance.EndFinancialPackageDate = DateTime.Now.AddMonths(userFinance.FinancialPackage.Term);
            userFinance.DayCount = (userFinance.EndFinancialPackageDate - userFinance.ChoicePackageDate).Days;

            var profitAmount = userFinance.AmountInPackage * (decimal)financialPackage.ProfitPercent / 100;

            userFinance.ProfitAmountPerDay = profitAmount / userFinance.DayCount;

            try
            {
                await mediator.Send(new CreateUserFinancialPackageAsync.Command(userFinance));
                return true;
            }
            catch (Exception)
            {
                throw;
            }

        }

        #region helper

        /// <summary>
        /// Gives financial packages for each user
        /// </summary>
        /// <param name="context"></param>
        /// <param name="UF"></param>
        /// <returns></returns>
        private static async Task<FinancialPackage> GetFinancialPackage(
              FinancialPackage fp
            , IMediator mediator) =>
            await mediator.Send(new FirstOrDefaultFinancialPackageAsync.Query(x => x.Id == fp.Id));


        private static async Task<bool> DontHaveEnoughMony(
              AppUser user
            , UserFinancialDTO financialDTO
            , IMediator mediator)
        {
            decimal sumAmountInPackages = await SumAmountInPackages(user, mediator);

            if (sumAmountInPackages == 0)
            {
                return user.AccountBalance < financialDTO.AmountInPackage ? true : false;
            }

            return user.AccountBalance < sumAmountInPackages + financialDTO.AmountInPackage ? true : false;
        }


        private static async Task<decimal> SumAmountInPackages(AppUser user, IMediator mediator)
        {
            decimal sumAmountInPackages = 0;

            //var userFinancials = await mediator.Send(new WhereUserFinancialPackage.Query(x => x.UserId == user.Id));
            var userFinancials1 = await mediator.Send(new GetAllUserFinancialPackagesAsync.Query());
            var userFinancials = userFinancials1.Where(x => x.UserId == user.Id);

            //var userFinancials = _userFinancial
            //     .Where(x => x.UserId == user.Id);

            foreach (var finance in userFinancials)
            {
                sumAmountInPackages += finance.AmountInPackage;
            }
            return sumAmountInPackages;
        }
        #endregion

    }
}
