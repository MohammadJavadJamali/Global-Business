#region using
using Dapper;
using MediatR;
using System.Data;
using System.Linq;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
#endregion

namespace Application.Transactions
{
    public class GetAllTransactionsAsync
    {
        public record Query : IRequest<List<Transaction>>;

        public class Handler : IRequestHandler<Query, List<Transaction>>
        {
            #region ctor
            private readonly IDbConnection _dbConnection;
            public Handler(IDbConnection dbConnection)
            {
                _dbConnection = dbConnection;
            }
            #endregion

            public async Task<List<Transaction>> Handle(Query request, CancellationToken cancellationToken)
            {
                var sql = "SELECT * FROM Transactions";
                
                _dbConnection.Open();

                var transactions = (await _dbConnection.QueryAsync<Transaction>(sql)).ToList();
                
                _dbConnection.Close();

                return transactions;
            }
        }
    }
}
