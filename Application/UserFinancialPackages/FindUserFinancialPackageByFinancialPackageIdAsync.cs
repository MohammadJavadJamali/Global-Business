#region using
using Dapper;
using MediatR;
using System.Data;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
#endregion

namespace Application.UserFinancialPackages
{
    /// <summary>
    /// Find a UserFinancialPackage by FinancialPackageId
    /// </summary>
    public class FindUserFinancialPackageByFinancialPackageIdAsync
    {
        public record Query(int Id) : IRequest<UserFinancialPackage>;

        public class Handler : IRequestHandler<Query, UserFinancialPackage>
        {
            #region ctor
            private readonly IDbConnection _dbConnection;
            public Handler(IDbConnection dbConnection)
            {
                _dbConnection = dbConnection;
            }
            #endregion 

            public async Task<UserFinancialPackage> Handle(Query request, CancellationToken cancellationToken)
            {
                var sql = "SELECT * FROM UserFinancialPackages WHERE FinancialPackageId = @Id";

                _dbConnection.Open();

                var userFinancialPackage = await _dbConnection.QueryFirstOrDefaultAsync(sql, new { Id = request.Id });

                _dbConnection.Close();

                return userFinancialPackage;
            }
        }
    }
}
