#region usings
using Dapper;
using MediatR;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Z.Dapper.Plus;
using Persistance;
using System.Data;
#endregion

namespace Application.Nodes
{
    public class UpdateNodeAsync
    {
        public record Command(Node Node) : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            #region Ctor
            private readonly IDbConnection _dbConnection;
            public Handler(IDbConnection dbConnection)
            {
                _dbConnection = dbConnection;
            }
            #endregion

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                #region sql
                var sql = "UPDATE Nodes SET TotalMoneyInvested = CONVERT(decimal(18, 4), @TotalMoneyInvested), " +
                    "TotalMoneyInvestedBySubsets = CONVERT(decimal(18, 4), @TotalMoneyInvestedBySubsets), " +
                    "MinimumSubBrachInvested = CONVERT(decimal(18, 4), @MinimumSubBrachInvested), " +
                    "LeftUserId = @LeftUserId, RightUserId = @RightUserId, IsCalculate = @IsCalculate" +
                    " WHERE Id = @Id";
                #endregion

                #region parameters
                var parameters = new
                {
                    request.Node.TotalMoneyInvested,
                    request.Node.TotalMoneyInvestedBySubsets,
                    request.Node.MinimumSubBrachInvested,
                    request.Node.LeftUserId,
                    request.Node.RightUserId,
                    request.Node.IsCalculate,
                    request.Node.Id
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
