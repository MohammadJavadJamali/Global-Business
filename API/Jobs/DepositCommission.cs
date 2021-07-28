#region using
using Quartz;
using MediatR;
using System.Linq;
using Domain.Model;
using Application.Nodes;
using System.Diagnostics;
using Application.Helpers;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Application.Users;
#endregion

namespace API.Jobs
{
    public class DepositCommission : IJob
    {

        #region Fields
        private readonly ILogger<DepositCommission> _logger;
        private readonly IMediator _mediator;
        #endregion

        #region Ctor
        public DepositCommission(ILogger<DepositCommission> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        #endregion

        #region work

        public async Task Execute(IJobExecutionContext context)
        {
            var watch = new Stopwatch();
            watch.Start();

            var rootNode = await _mediator.Send(new FirstOrDefaultNodeAsync.Query(x => x.ParentId == null));

            var watch1 = new Stopwatch();
            watch1.Start();

            await recursive(rootNode);

            watch1.Stop();
            _logger.LogInformation($"time to traverse tree : {watch1.ElapsedMilliseconds} ms");

            //var nodes = await _node.GetAll(n => n.TotalMoneyInvestedBySubsets > 0);
            var nodes = await _mediator.Send(new GetAllNodesAsync.Query());

            var watch2 = new Stopwatch();
            watch1.Start();

            foreach (var node in nodes)
            {
                node.TotalMoneyInvestedBySubsets = 0;
                node.MinimumSubBrachInvested = 0;
                node.IsCalculate = true;

                await _mediator.Send(new UpdateNodeAsync.Command(node));
            }
            watch2.Stop();
            _logger.LogInformation($"time to set zero : {watch2.ElapsedMilliseconds} ms");

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
                leftNode = await _mediator.Send(new FirstOrDefaultNodeAsync.Query(x => x.AppUser.Id == node.LeftUserId));

                await recursive(leftNode);
            }
            if (node.RightUserId is not null && node.AppUser.CommissionPaid is false)
            {
                var commission = node.MinimumSubBrachInvested * 10 / 100;

                node.AppUser.CommissionPaid = true;
                await _mediator.Send(new UpdateUserAsync.Command(node.AppUser));

                if (commission is not 0)
                {
                    await ProfitHelper.CreateProfit(node.AppUser, commission, _mediator);
                    await TransactionHelper.CreateTransaction(node.AppUser, commission, _mediator);
                }

                rightNode = await _mediator.Send(new FirstOrDefaultNodeAsync.Query(x => x.AppUser.Id == node.RightUserId));

                await recursive(rightNode);
            }

        }

        #endregion
    }
}
