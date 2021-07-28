#region using
using Dapper;
using MediatR;
using System.Data;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
#endregion

namespace Application.Nodes
{
    public class CreateNodeAsync
    {
        public record Command(Node Node) : IRequest;

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

                #region parameters
                var parameters = new
                {
                    request.Node.UserId,
                    request.Node.ParentId,
                    request.Node.LeftUserId,
                    request.Node.RightUserId,
                    request.Node.TotalMoneyInvested,
                    request.Node.TotalMoneyInvestedBySubsets,
                    request.Node.IntroductionCode,
                    request.Node.MinimumSubBrachInvested,
                    request.Node.IsCalculate
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
