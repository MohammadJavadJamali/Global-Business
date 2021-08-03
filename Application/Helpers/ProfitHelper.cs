using MediatR;
using Domain.Model;
using Application.Profits;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public static class ProfitHelper
    {
        public static async Task CreateProfit(
              AppUser user
            , decimal profitAmount
            , IMediator mediator)
        {
            Profit profit = new();

            profit.User = user;
            profit.User_Id = user.Id;
            profit.ProfitAmount = profitAmount;
            profit.ProfitDepositDate = System.DateTime.Now;

            await mediator.Send(new CreateProfitAsync.Command(profit));
        }
    }
}
