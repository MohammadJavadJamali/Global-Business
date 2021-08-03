#region using
using Dapper;
using MediatR;
using System.Data;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
#endregion

namespace Application.Transactions
{
    public class CreateListTransactionAsync
    {
        public record Command(List<Transaction> Transaction) : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            #region ctor
            private readonly IDbConnection _dbConnection;
            public Handler(IDbConnection dbConnection)
            {
                _dbConnection = dbConnection;
            }
            #endregion

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                #region sql
                var sql =
                    "INSERT INTO Transactions " +
                        "(InitialBalance, Amount, FinalBalance, TransactionDate, EmailTargetAccount, User_Id, IsDeleted)" +
                    "VALUES" +
                        "(@InitialBalance, @Amount, @FinalBalance, @TransactionDate, @EmailTargetAccount, @User_Id, @IsDeleted)";
                #endregion

                _dbConnection.Open();

                await _dbConnection.ExecuteAsync(sql, request.Transaction);
                
                _dbConnection.Close();

                return Unit.Value;
            }
        }
    }
}
