using Quartz;
using Domain.Model;
using System.Diagnostics;
using Application.Helpers;
using System.Threading.Tasks;
using Application.Repository;
using Microsoft.Extensions.Logging;

namespace API.Jobs
{

    public class DepositCommission : IJob
    {

        #region FIelds
        private readonly ISave _save;
        private readonly IUser _user;
        private readonly INode _node;
        private readonly IProfit _profit;
        private readonly ITransaction _transaction;
        private readonly IUserFinancial _userFinancial;
        private readonly ILogger<DepositCommission> _logger;
        private readonly IFinancialPackage _financialPackage;
        #endregion

        #region Ctor
        public DepositCommission(
              INode node
            , IProfit profit
            , ITransaction transaction
            , IUserFinancial userFinancial
            , ILogger<DepositCommission> logger
            , IUser user, IFinancialPackage financialPackage, ISave save)
        {
            _node = node;
            _user = user;
            _logger = logger;
            _profit = profit;
            _transaction = transaction;
            _userFinancial = userFinancial;
            _financialPackage = financialPackage;
            _save = save;
        }
        #endregion

        #region work

        public async Task Execute(IJobExecutionContext context)
        {
            var watch = new Stopwatch();
            watch.Start();

            var rootNode = await _node.FirstOrDefaultAsync(n => n.ParentId == null, x => x.AppUser);

            var watch1 = new Stopwatch();
            watch1.Start();

            await recursive(rootNode);
            //await _save.SaveChangeAsync();

            watch1.Stop();
            _logger.LogInformation($"time to traverse tree : {watch1.ElapsedMilliseconds} ms");

            var nodes = await _node.GetAll(n => n.TotalMoneyInvestedBySubsets > 0);

            foreach (var node in nodes)
            {
                node.TotalMoneyInvestedBySubsets = 0;
                node.MinimumSubBrachInvested = 0;
                node.IsCalculate = true;

                _node.Update(node);
            }

            await _save.SaveChangeAsync();

            watch.Stop();
            _logger.LogInformation($"time to pay commission : {watch.ElapsedMilliseconds} ms");
        }
        #endregion


        #region helper

        public Node leftNode { get; set; }

        public Node rightNode { get; set; }


        public async Task recursive(Node node)
        {
            if (node.LeftUserId is not null)
            {
                leftNode = await _node.FirstOrDefaultAsync(u => u.AppUser.Id == node.LeftUserId, x => x.AppUser);

                await recursive(leftNode);
            }
            if (node.RightUserId is not null && node.AppUser.CommissionPaid is false)
            {

                var commission = node.MinimumSubBrachInvested * 10 / 100;

                node.AppUser.CommissionPaid = true;
                _user.Update(node.AppUser);

                if (commission is not 0)
                {
                    await ProfitHelper.CreateProfit(node.AppUser, _profit, commission);
                    TransactionHelper.CreateTransaction(_user, node.AppUser, commission, _transaction);
                }

                rightNode = await _node
                    .FirstOrDefaultAsync(u => u.AppUser.Id == node.RightUserId, x => x.AppUser);

                await recursive(rightNode);
            }

        }

        #endregion
    }
}
