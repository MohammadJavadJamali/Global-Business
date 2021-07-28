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

namespace Application.UserFinancialPackages
{
    public class CreateUserFinancialPackageAsync
    {
        public record Command(UserFinancialPackage UserFinancialPackage) : IRequest;

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
                    "INSERT INTO UserFinancialPackages " +
                        "(UserId, FinancialPackageId, ChoicePackageDate, EndFinancialPackageDate, AmountInPackage, IsDeleted, ProfitAmountPerDay, DayCount)" +
                    "VALUES" +
                    "   (@UserId, @FinancialPackageId, @ChoicePackageDate, @EndFinancialPackageDate, @AmountInPackage, @IsDeleted, @ProfitAmountPerDay, @DayCount)";
                #endregion

                #region parameters
                var parameters = new
                {
                    request.UserFinancialPackage.UserId,
                    request.UserFinancialPackage.FinancialPackageId,
                    request.UserFinancialPackage.ChoicePackageDate,
                    request.UserFinancialPackage.EndFinancialPackageDate,
                    request.UserFinancialPackage.AmountInPackage,
                    request.UserFinancialPackage.IsDeleted,
                    request.UserFinancialPackage.ProfitAmountPerDay,
                    request.UserFinancialPackage.DayCount
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
