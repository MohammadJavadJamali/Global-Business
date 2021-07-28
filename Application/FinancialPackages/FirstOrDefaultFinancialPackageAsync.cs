#region using
using Dapper;
using System;
using MediatR;
using System.Data;
using System.Linq;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
#endregion

namespace Application.FinancialPackages
{
    public class FirstOrDefaultFinancialPackageAsync
    {
        public record Query(Func<FinancialPackage, bool> expression) : IRequest<FinancialPackage>;

        public class Handler : IRequestHandler<Query, FinancialPackage>
        {
            #region ctor
            private readonly IDbConnection _dbConnection;
            public Handler(IDbConnection dbConnection)
            {
                _dbConnection = dbConnection;
            }
            #endregion

            public async Task<FinancialPackage> Handle(Query request, CancellationToken cancellationToken)
            {
                var sql = "SELECT * FROM FinancialPackages";

                _dbConnection.Open();

                var financialPackages = (await _dbConnection.QueryAsync<FinancialPackage>(sql)).ToList();

                _dbConnection.Close();

                return financialPackages.FirstOrDefault(request.expression);
            }
        }
    }
}
