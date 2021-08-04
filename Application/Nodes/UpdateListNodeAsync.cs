#region usings
using Dapper;
using MediatR;
using System.Data;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
#endregion

namespace Application.Nodes
{
    public class UpdateListNodeAsync
    {
        public record Command(List<Node> Nodes) : IRequest<int>;

        public class Handler : IRequestHandler<Command, int>
        {
            #region Ctor
            private readonly IDbConnection _dbConnection;
            public Handler(IDbConnection dbConnection)
            {
                _dbConnection = dbConnection;
            }
            #endregion

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                #region sql
                var sql = "UPDATE Nodes SET TotalMoneyInvested = CONVERT(decimal(18, 4), @TotalMoneyInvested), " +
                    "TotalMoneyInvestedBySubsets = CONVERT(decimal(18, 4), @TotalMoneyInvestedBySubsets), " +
                    "MinimumSubBrachInvested = CONVERT(decimal(18, 4), @MinimumSubBrachInvested), " +
                    "LeftUserId = @LeftUserId, RightUserId = @RightUserId, IsCalculate = @IsCalculate" +
                    " WHERE Id = @Id";
                #endregion
                
                _dbConnection.Open();

                var res = await _dbConnection.ExecuteAsync(sql, request.Nodes);

                _dbConnection.Close();

                return res;
            }
        }
    }
}
