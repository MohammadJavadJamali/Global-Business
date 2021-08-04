#region using
using Dapper;
using MediatR;
using System.Data;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
#endregion

namespace Application.FinancialPackages
{
    public class UpdateListFinancialPackageAsync
    {
        public record Command(List<FinancialPackage> financialPackages) : IRequest<int>;

        public class Handler : IRequestHandler<Command, int>
        {
            #region ctor
            private readonly IDbConnection _dbConnection;
            public Handler(IDbConnection dbConnection)
            {
                _dbConnection = dbConnection;
            }
            #endregion

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                #region sql
                var sql =
                    "UPDATE FinancialPackages " +
                    "SET " +
                        "ProfitPercent = @ProfitPercent, Term = @Term WHERE Id = @Id";
                #endregion

                FinancialPackage[] financialPackages = request.financialPackages.ToArray();

                _dbConnection.Open();

                var res = await _dbConnection.ExecuteAsync(sql, financialPackages);

                _dbConnection.Close();

                return res;

            }
        }
    }
}
