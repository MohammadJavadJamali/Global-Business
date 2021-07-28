using System;
using MediatR;
using Domain.Model;
using Application.Users;
using Application.Transactions;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public static class TransactionHelper
    {
        /// <summary>
        /// create a transaction and update user account balance . 
        /// If you want to withdraw or transfer from the account, enter a negative transactionAmount
        /// </summary>
        /// <param name="_user"></param>
        /// <param name="user"></param>
        /// <param name="transactionAmount"></param>
        /// <param name="_transaction"></param>
        /// <returns></returns>
        public static async Task CreateTransaction(
              AppUser user
            , decimal transactionAmount
            , IMediator mediator)
        {
            Transaction transaction = new();

            transaction.User = user;
            transaction.User_Id = user.Id;
            transaction.Amount = transactionAmount;
            transaction.TransactionDate = DateTime.Now;
            transaction.EmailTargetAccount = user.Email;
            transaction.InitialBalance = user.AccountBalance;
            transaction.FinalBalance = user.AccountBalance + transactionAmount;

            user.AccountBalance += transactionAmount;

            await mediator.Send(new CreateTransactionAsync.Command(transaction));
            await mediator.Send(new UpdateUserAsync.Command(user));

        }
    }
}
