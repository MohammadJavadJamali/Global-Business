#region using
using Dapper;
using MediatR;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
#endregion

namespace Application.FinancialPackages
{
    public class RemoveFinancialPackages
    {
        public record Command(int Id) : IRequest;

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

                var sql = "DELETE FROM FinancialPackages WHERE Id = @Id";

                _dbConnection.Open();

                await _dbConnection.ExecuteAsync(sql, new { request.Id });
                
                _dbConnection.Close();


                return Unit.Value;
            }
        }
    }
}
