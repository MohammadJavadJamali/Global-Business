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

namespace Application.Profits
{
    public class GetAllProfitsAsync
    {
        public record Query : IRequest<List<Profit>>;

        public class Handler : IRequestHandler<Query, List<Profit>>
        {
            #region ctor
            private readonly IDbConnection _dbConnection;
            public Handler(IDbConnection dbConnection)
            {
                _dbConnection = dbConnection;
            }
            #endregion

            public async Task<List<Profit>> Handle(Query request, CancellationToken cancellationToken)
            {
                var sql = "SELECT * FROM Profits ";

                _dbConnection.Open();

                var Profits = (await _dbConnection.QueryAsync<Profit>(sql)).ToList();

                _dbConnection.Close();

                return Profits;
            }
        }
    }
}
