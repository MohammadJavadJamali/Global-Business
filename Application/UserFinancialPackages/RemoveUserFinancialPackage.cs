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
    public class RemoveUserFinancialPackage
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
                    "DELETE FROM UserFinancialPackages" +
                    " WHERE UserId = @UserId And FinancialPackageId = @FinancialpackageId";
                #endregion

                #region parameters
                var parameters = new
                {
                    request.UserFinancialPackage.UserId,
                    request.UserFinancialPackage.FinancialPackageId
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

