#region using
using Dapper;
using MediatR;
using System.Data;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
#endregion

namespace Application.FinancialPackages
{
    public class FindFinancialPackageByIdAsync
    {
        public record Query(int Id) : IRequest<FinancialPackage>;

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
                var sql = "SELECT * FROM FinancialPackages WHERE Id = @Id";

                _dbConnection.Open();

                var financialPackage = await _dbConnection
                    .QuerySingleOrDefaultAsync<FinancialPackage>(sql, new { request.Id });

                _dbConnection.Close();


                return financialPackage;

            }
        }
    }
}
