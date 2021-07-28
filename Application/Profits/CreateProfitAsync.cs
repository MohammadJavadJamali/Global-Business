#region using
using Dapper;
using MediatR;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
#endregion

namespace Application.Profits
{
    public class CreateProfitAsync
    {
        public record Command(Profit Profits) : IRequest;

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
                var sql = "INSERT INTO Profits (ProfitDepositDate, ProfitAmount, IsDeleted, User_Id)" +
                    "VALUES(@ProfitDepositDate, @ProfitAmount, @IsDeleted, @User_Id)";
                #endregion

                #region params
                var parameters = new
                {
                    request.Profits.ProfitDepositDate,
                    request.Profits.ProfitAmount,
                    request.Profits.IsDeleted,
                    request.Profits.User_Id
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
