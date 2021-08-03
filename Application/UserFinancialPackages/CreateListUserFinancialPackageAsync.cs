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
    public class CreateListUserFinancialPackageAsync
    {
        public record Command(List<UserFinancialPackage> UserFinancialPackage) : IRequest;

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

                _dbConnection.Open();

                await _dbConnection.ExecuteAsync(sql, request.UserFinancialPackage);
                
                _dbConnection.Close();

                return Unit.Value;
            }
        }
    }
}
