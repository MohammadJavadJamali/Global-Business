#region using
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
    public class CreateListNodeAsync
    {
        public record Command(List<Node> Nodes) : IRequest;

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
                var sql = "INSERT INTO Nodes " +
                            "(UserId, ParentId, LeftUserId, RightUserId, TotalMoneyInvested, TotalMoneyInvestedBySubsets, IntroductionCode, MinimumSubBrachInvested, IsCalculate) " +
                      "VALUES(@UserId, @ParentId, @LeftUserId, @RightUserId, @TotalMoneyInvested, @TotalMoneyInvestedBySubsets, @IntroductionCode, @MinimumSubBrachInvested, @IsCalculate)";
                #endregion
                
                _dbConnection.Open();

                await _dbConnection.ExecuteAsync(sql, request.Nodes);

                _dbConnection.Close();

                return Unit.Value;
            }
        }
    }
}
