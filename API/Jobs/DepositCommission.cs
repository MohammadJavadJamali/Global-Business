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
            bool done = false;
            do
            {
                var start = DateTime.Now;

                var workTime = new Stopwatch();
                workTime.Start();

                var rootNode = await _mediator.Send(new FirstOrDefaultNodeAsync.Query(x => x.ParentId == null));

                var totalCommision = await recursive(rootNode);

                var profitResult = await _mediator.Send(new CreateListProfitAsync.Command(profits));
                if (profitResult < 0)
                {
                    WriteLog(0, totalCommision, SP.Fail, start, DateTime.Now, SP.Commission
                        , "problem in create profits");

                    continue;
                }

                var transactionResult = await _mediator.Send(new CreateListTransactionAsync.Command(transactions));
                if (transactionResult < 0)
                {
                    WriteLog(0, totalCommision, SP.Fail, start, DateTime.Now, SP.Commission
                        , "problem in create transactions");
                    continue;
                }

                var userResult = await _mediator.Send(new UpdateListUserAsync.Command(users));
                if (userResult < 0)
                {
                    WriteLog(0, totalCommision, SP.Fail, start, DateTime.Now, SP.Commission
                         , "problem in update users");

                    continue;
                }

                var nodes = await _mediator.Send(new GetAllNodesAsync.Query());

                foreach (var node in nodes)
                {
                    node.TotalMoneyInvestedBySubsets = 0;
                    node.MinimumSubBrachInvested = 0;
                    node.IsCalculate = true;
                }

                var nodeResult = await _mediator.Send(new UpdateListNodeAsync.Command(nodes));
                if (nodeResult < 0)
                {
                    WriteLog(0, totalCommision, SP.Fail, start, DateTime.Now, SP.Commission
                        , "problem in update nodes");
                    continue;
                }

                workTime.Stop();

                WriteLog(nodes.Where(n => n.AppUser.CommissionPaid is false).Count(), 
                    totalCommision, SP.Success, start, DateTime.Now, SP.Commission
                        , $"time to pay commission : {workTime.ElapsedMilliseconds} ms");
                
                done = true;

            } while (!done);
        }
        #endregion


        #region helper

        public Node leftNode { get; set; }
        public Node rightNode { get; set; }

        List<Transaction> transactions = new();

        IQueryable<Profit> profits ;

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
                    #region transaction
                    Transaction transaction = new();
                    transaction.Amount = commission;
                    transaction.InitialBalance = node.AppUser.AccountBalance;
                    transaction.FinalBalance = node.AppUser.AccountBalance + commission;
                    transaction.EmailTargetAccount = node.AppUser.Email;
                    transaction.User = node.AppUser;
                    transaction.User_Id = node.AppUser.Id;
                    transaction.TransactionDate = DateTime.Now;
                    #endregion

                    node.AppUser.AccountBalance += commission;

                    #region profit
                    Profit profit = new();
                    profit.User = node.AppUser;
                    profit.User_Id = node.AppUser.Id;
                    profit.ProfitAmount = commission;
                    profit.ProfitDepositDate = DateTime.Now;
                    #endregion

                    #region add to list
                    transactions.Add(transaction);
                    profits.Append<Profit>(profit);
                    users.Add(node.AppUser);
                    #endregion

                    totalCommision += commission;
                }

                rightNode = await _mediator
                    .Send(new FirstOrDefaultNodeAsync.Query(x => x.AppUser.Id == node.RightUserId));

                await recursive(rightNode);
            }

            return totalCommision;
        }
        #endregion

        private void WriteLog(int count, decimal totalCommision, string status, DateTime start,
            DateTime end, string type, string message)
        {
            Log.Logger
                .ForContext("Count", count)
                .ForContext("TotalDeposit", totalCommision)
                .ForContext("Status", status)
                .ForContext("Start", start)
                .ForContext("End", end)
                .ForContext("Type", type)
                .Warning(message);
        }
    }
}
