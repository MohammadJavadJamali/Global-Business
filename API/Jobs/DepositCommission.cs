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
using System;
using System.Collections.Generic;
using Application.Profits;
using Application.Transactions;
using Serilog;
using Application;
#endregion

namespace API.Jobs
{
    public class DepositCommission : IJob
    {

        #region Fields
        private readonly IMediator _mediator;
        private readonly ILogger<DepositCommission> _logger;
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
            var start = DateTime.Now;

            var workTime = new Stopwatch();
            workTime.Start();


            var rootNode = await _mediator.Send(new FirstOrDefaultNodeAsync.Query(x => x.ParentId == null));

            var totalCommision = await recursive(rootNode);

            await _mediator.Send(new CreateListProfitAsync.Command(profits));
            await _mediator.Send(new CreateListTransactionAsync.Command(transactions));
            await _mediator.Send(new UpdateListUserAsync.Command(users));

            var nodes = await _mediator.Send(new GetAllNodesAsync.Query());

            foreach (var node in nodes)
            {
                node.TotalMoneyInvestedBySubsets = 0;
                node.MinimumSubBrachInvested = 0;
                node.IsCalculate = true;
            }

            await _mediator.Send(new UpdateListNodeAsync.Command(nodes));


            workTime.Stop();

            Log.Logger
               .ForContext("Count", nodes.Where(n => n.AppUser.CommissionPaid is false).Count())
               .ForContext("TotalDeposit", totalCommision)
               .ForContext("Status", "Success")
               .ForContext("Start", start)
               .ForContext("End", DateTime.Now)
               .ForContext("Type", SP.Commission)
               .Information($"time to pay commission : {workTime.ElapsedMilliseconds} ms");

        }
        #endregion


        #region helper

        public Node leftNode { get; set; }
        public Node rightNode { get; set; }

        List<Transaction> transactions = new();
        List<Profit> profits = new();
        List<AppUser> users = new();
        decimal totalCommision = 0;

        public async Task<decimal> recursive(Node node)
        {
            if (node.LeftUserId is not null)
            {
                leftNode = await _mediator
                    .Send(new FirstOrDefaultNodeAsync.Query(x => x.AppUser.Id == node.LeftUserId));
                
                await recursive(leftNode);
            }
            if (node.RightUserId is not null && node.AppUser.CommissionPaid is false)
            {
                var commission = node.MinimumSubBrachInvested * 10 / 100;

                node.AppUser.CommissionPaid = true;

                if (commission is not 0)
                {
                    Transaction transaction = new();
                    transaction.Amount = commission;
                    transaction.InitialBalance = node.AppUser.AccountBalance;
                    transaction.FinalBalance = node.AppUser.AccountBalance + commission;
                    transaction.EmailTargetAccount = node.AppUser.Email;
                    transaction.User = node.AppUser;
                    transaction.User_Id = node.AppUser.Id;
                    transaction.TransactionDate = DateTime.Now;

                    node.AppUser.AccountBalance += commission;

                    Profit profit = new();
                    profit.User = node.AppUser;
                    profit.User_Id = node.AppUser.Id;
                    profit.ProfitAmount = commission;
                    profit.ProfitDepositDate = DateTime.Now;

                    transactions.Add(transaction);
                    profits.Add(profit);
                    users.Add(node.AppUser);

                    totalCommision += commission;
                }

                rightNode = await _mediator
                    .Send(new FirstOrDefaultNodeAsync.Query(x => x.AppUser.Id == node.RightUserId));

                await recursive(rightNode);
            }

            return totalCommision;
        }
        #endregion
    }
}
