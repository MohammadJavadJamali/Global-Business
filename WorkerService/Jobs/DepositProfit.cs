using Quartz;
using Domain.Model;
using Application.Helpers;
using Application.Repository;

namespace WorkerService.Jobs
{
    [DisallowConcurrentExecution]
    public class DepositProfit : IJob
    {
        private readonly ISave _save;
        private readonly IUser _user;
        private readonly IProfit _profit;
        private readonly ITransaction _transaction;
        private readonly IUserFinancial _userFinancial;
        private readonly ILogger<DepositProfit> _logger;

        public DepositProfit(
              ISave save
            , IUser user
            , IProfit profit
            , ITransaction transaction
            , IUserFinancial userFinancial
            , ILogger<DepositProfit> logger)
        {
            _save = save;
            _user = user;
            _logger = logger;
            _profit = profit;
            _transaction = transaction;
            _userFinancial = userFinancial;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await CalculateProfitAmountPerDayForEachUser();
        }

        private async Task CalculateProfitAmountPerDayForEachUser()
        {
            Console.WriteLine("Deposit Profit worked.");

            Profit profit = new()
            {
                ProfitAmount = 10000,
                UserId = "286a858d-60c4-4b99-bf75-85f8b7a2e7fb",
                ProfitDepositDate = DateTime.Now,
                IsDeleted = false
            };
            await _profit.CreateAsync(profit);

            await Task.Delay(2000);

            var users = await GetAllUsers();

            foreach (var user in users)
            {

                var userFinancialPackages = user.UserFinancialPackages;

                foreach (var UF in userFinancialPackages)
                {
                    if (!IsEndFinancialPackage(UF))
                    {
                        var profitAmountPerDay = UF.ProfitAmountPerDay;

                        TransactionHelper.CreateTransaction(_user, user, profitAmountPerDay, _transaction);
                        await ProfitHelper.CreateProfit(user, _profit, profitAmountPerDay);
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

            _logger.LogInformation($"deposit profit for {users.Count()} users");

        }

        private bool IsEndFinancialPackage(UserFinancialPackage UF) =>
            UF.EndFinancialPackageDate <= DateTime.Now ? true : false;

        private async Task<IEnumerable<AppUser>> GetAllUsers() =>
            await _user.GetAll(x => x.UserFinancialPackages.Count() > 0, y => y.UserFinancialPackages);

    }
}
