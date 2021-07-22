using Domain.Model;
using Application.Repository;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public static class ProfitHelper
    {
        public static async Task CreateProfit(
              AppUser user
            , IProfit _profit
            , decimal profitAmount)
        {
            Profit profit = new();
            //date time set in Create method
            profit.User = user;
            profit.ProfitAmount = profitAmount;

            await _profit.Create(profit);
        }
    }
}
