using Domain.Model;
using Application.Repository;
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
        public static void CreateTransaction(
              IUser _user
            , AppUser user
            , decimal transactionAmount
            , ITransaction _transaction)
        {
            Transaction transaction = new();
            //transaction date set in CreteAsync method
            transaction.User = user;
            transaction.Amount = transactionAmount;
            transaction.EmailTargetAccount = user.Email;
            transaction.InitialBalance = user.AccountBalance;
            transaction.FinalBalance = user.AccountBalance + transactionAmount;

            user.AccountBalance += transactionAmount;

            _transaction.Create(transaction);

            _user.Update(user);
        }
    }
}
