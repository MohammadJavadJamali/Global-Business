#region using
using Dapper;
using MediatR;
using System.Data;
using System.Linq;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
#endregion

namespace Application.FinancialPackages
{
    public class GetAllFinancialPackagesAsync
    {
        public record Query() : IRequest<List<FinancialPackage>>;


        public class Handler : IRequestHandler<Query, List<FinancialPackage>>
        {
            #region ctor
            private readonly IDbConnection _dbConnection;
            public Handler(IDbConnection dbConnection)
            {
                _dbConnection = dbConnection;
            }
            #endregion

            public async Task<List<FinancialPackage>> Handle(Query request, CancellationToken cancellationToken)
            {
                var sql = "SELECT * FROM FinancialPackages";

                _dbConnection.Open();

                var financialPackages = (await _dbConnection.QueryAsync<FinancialPackage>(sql)).ToList();
                
                _dbConnection.Close();


                return financialPackages;
            }
        }
    }
}
