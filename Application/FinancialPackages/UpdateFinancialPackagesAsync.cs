#region using
using Dapper;
using MediatR;
using System.Data;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
#endregion

namespace Application.FinancialPackages
{
    public class UpdateFinancialPackagesAsync
    {
        public record Command(FinancialPackage financialPackage) : IRequest;

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
                    "UPDATE FinancialPackages " +
                    "SET " +
                        "ProfitPercent = @ProfitPercent, Term = @Term WHERE Id = @Id";
                #endregion

                #region parameters
                var parameters = new
                {
                    request.financialPackage.ProfitPercent,
                    request.financialPackage.Term,
                    request.financialPackage.Id
                };
                #endregion

                _dbConnection.Open();

                await _dbConnection.ExecuteAsync(sql, parameters);

                _dbConnection.Close();

                return Unit.Value;

            }
        }
    }
}
