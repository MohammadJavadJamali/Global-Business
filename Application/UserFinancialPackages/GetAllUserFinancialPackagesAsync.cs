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

namespace Application.Nodes
{
    public class GetAllUserFinancialPackagesAsync
    {
        public record Query : IRequest<List<UserFinancialPackage>>;

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

                return userFinancialPackages;
            }
        }
    }
}
