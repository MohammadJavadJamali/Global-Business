#region using
using Dapper;
using MediatR;
using System.Data;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
#endregion

namespace Application.UserFinancialPackages
{
    public class UpdateListUserFinancialPackageAsync
    {
        public record Command(List<UserFinancialPackage> UserFinancialPackages) : IRequest;

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

                _dbConnection.Open();

                await _dbConnection.ExecuteAsync(sql, request.UserFinancialPackages);
                
                _dbConnection.Close();

                return Unit.Value;
            }
        }
    }
}
