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
    public class UpdateUserFinancialPackageAsync
    {
        public record Command(UserFinancialPackage UserFinancialPackages) : IRequest;

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
                var sql = "UPDATE UserFinancialPackages SET " +
                    "ChoicePackageDate = @ChoicePackageDate, EndFinancialPackageDate = @EndFinancialPackageDate, " +
                    "AmountInPackage = @AmountInPackage, IsDeleted = @IsDeleted, ProfitAmountPerDay = @ProfitAmountPerDay, " +
                    "DayCount = @DayCount WHERE UserId = '@UserId' AND FinancialPackageId = @FinancialPackageId ";
                #endregion

                #region parameters
                var parameters = new
                {
                    request.UserFinancialPackages.ChoicePackageDate,
                    request.UserFinancialPackages.EndFinancialPackageDate,
                    request.UserFinancialPackages.AmountInPackage,
                    request.UserFinancialPackages.IsDeleted,
                    request.UserFinancialPackages.ProfitAmountPerDay,
                    request.UserFinancialPackages.DayCount,
                    request.UserFinancialPackages.UserId,
                    request.UserFinancialPackages.FinancialPackageId
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
