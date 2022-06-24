using Quartz;
using Domain.Model;
using Application.Repository;

namespace WorkerService.Jobs
{

    public class DepositCommission : IJob
    {

        private readonly ISave _save;
        private readonly IUser _user;
        private readonly INode _node;
        private readonly IProfit _profit;
        private readonly ITransaction _transaction;

        public DepositCommission(
              INode node
            , ISave save
            , IProfit profit
            , ITransaction transaction
            , IUser user)
        {
            _node = node;
            _user = user;
            _save = save;
            _profit = profit;
            _transaction = transaction;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Deposit Commission worked.");

            var rootNode = await _node.FirstOrDefaultAsync(n => n.ParentId == null, x => x.AppUser);

            await recursive(rootNode);

            var nodes = await _node.GetAll();

            foreach (var node in nodes)
            {
                node.TotalMoneyInvestedBySubsets = 0;
                node.MinimumSubBrachInvested = 0;
                node.IsCalculate = true;

                _node.Update(node);
            }

            await _save.SaveChangeAsync();
        }

        public Node? leftNode { get; set; }

        public Node? rightNode { get; set; }


        public async Task recursive(Node node)
        {
            if (node is not null && node.LeftUserId is not null)
            {
                leftNode = await _node.FirstOrDefaultAsync(u => u.AppUser.Id == node.LeftUserId, x => x.AppUser);

                await recursive(leftNode);
            }
            if (node is not null && node.RightUserId is not null && node.AppUser.CommissionPaid is false)
            {

                var commission = node.MinimumSubBrachInvested * 10 / 100;

                node.AppUser.CommissionPaid = true;
                _user.Update(node.AppUser);

                if (commission is not 0)
                {
                    Profit profit = new();
                    profit.User = node.AppUser;
                    profit.ProfitAmount = commission;
                    await _profit.Create(profit);

                    Transaction transaction = new();
                    transaction.User = node.AppUser;
                    transaction.Amount = commission;
                    transaction.EmailTargetAccount = node.AppUser.Email;
                    transaction.InitialBalance = node.AppUser.AccountBalance;
                    transaction.FinalBalance = node.AppUser.AccountBalance + commission;

                    node.AppUser.AccountBalance += commission;

                    _transaction.Create(transaction);

                    _user.Update(node.AppUser);
                }

                rightNode = await _node
                    .FirstOrDefaultAsync(u => u.AppUser.Id == node.RightUserId, x => x.AppUser);

                await recursive(rightNode);
            }

        }
    }
}
