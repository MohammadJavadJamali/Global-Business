using Domain.DTO;
using Domain.Model;

namespace Application.Helpers
{
    public static class MapTransactionHelper
    {
        public static Transaction MapTransaction(AppUser currentUser, TransactionDTo transactionDTo)
        {
            Transaction transaction = new();

            transaction.User = currentUser;
            transaction.Amount = transactionDTo.Amount;
            transaction.EmailTargetAccount = currentUser.Email;
            transaction.InitialBalance = currentUser.AccountBalance;
            transaction.FinalBalance = currentUser.AccountBalance + transactionDTo.Amount;

            return transaction;
        }
    }
}
