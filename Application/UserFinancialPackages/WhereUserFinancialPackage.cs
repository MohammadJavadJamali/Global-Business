#region using
using Dapper;
using System;
using MediatR;
using System.Data;
using System.Linq;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
#endregion

namespace Application.FinancialPackages
{
    public class WhereUserFinancialPackage
    {
        public record Query(Func<UserFinancialPackage, bool> expression) : IRequest<List<UserFinancialPackage>>;

        public class Handler : IRequestHandler<Query, List<UserFinancialPackage>>
        {
            #region ctor
            private readonly IDbConnection _dbConnection;
            public Handler(IDbConnection dbConnection)
            {
                _dbConnection = dbConnection;
            }
            #endregion

            public async Task<List<UserFinancialPackage>> Handle(Query request, CancellationToken cancellationToken)
            {
                var sql = "SELECT * FROM UserFinancialPackages";

                _dbConnection.Open();

                var userFinancialPackages = (await _dbConnection.QueryAsync<UserFinancialPackage>(sql)).ToList();
                
                _dbConnection.Close();

                var userFinancialPackage = userFinancialPackages.Where(request.expression).ToList();

                return userFinancialPackage;
            }
        }
    }
}
