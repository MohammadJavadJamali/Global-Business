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
    public class CreateFinancialPackageAsync
    {
        public record Command(FinancialPackage FinancialPackage) : IRequest;

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
                var sql = "INSERT INTO FinancialPackages (ProfitPercent, Term, IsDeleted) " +
                    "VALUES(@ProfitPercent, @Term, @IsDeleted)";

                _dbConnection.Open();

                await _dbConnection.ExecuteAsync(sql, new
                {
                    request.FinancialPackage.ProfitPercent,
                    request.FinancialPackage.Term,
                    request.FinancialPackage.IsDeleted
                });

                _dbConnection.Close();

                return Unit.Value;
            }
        }
    }
}
