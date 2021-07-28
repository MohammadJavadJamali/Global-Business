#region using
using Dapper;
using MediatR;
using System.Data;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
#endregion

namespace Application.Transactions
{
    public class FindTransactionByIdAsync
    {
        public record Query(int Id) : IRequest<Transaction>;

        public class Handler : IRequestHandler<Query, Transaction>
        {
            #region ctor
            private readonly IDbConnection _dbConnection;
            public Handler(IDbConnection dbConnection)
            {
                _dbConnection = dbConnection;
            }
            #endregion

            public async Task<Transaction> Handle(Query request, CancellationToken cancellationToken)
            {
                var sql = "SELECT * FROM Transactions WHERE Id = @Id";
                
                _dbConnection.Open();

                var transaction = await _dbConnection.QueryFirstOrDefaultAsync(sql, new { Id = request.Id });

                _dbConnection.Close();

                return transaction;
            }
        }
    }
}
