#region using
using Dapper;
using MediatR;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Collections.Generic;
using System.Linq;
#endregion

namespace Application.Profits
{
    public class CreateListProfitAsync
    {
        public record Command(IQueryable<Profit> Profits) : IRequest<int>;

        public class Handler : IRequestHandler<Command, int>
        {
            #region ctor
            private readonly IDbConnection _dbConnection;
            public Handler(IDbConnection dbConnection)
            {
                _dbConnection = dbConnection;
            }
            #endregion

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                #region sql
                var sql = "INSERT INTO Profits (ProfitDepositDate, ProfitAmount, IsDeleted, User_Id)" +
                    "VALUES(@ProfitDepositDate, @ProfitAmount, @IsDeleted, @User_Id)";
                #endregion
                
                _dbConnection.Open();

                var res = await _dbConnection.ExecuteAsync(sql, request.Profits);
                
                _dbConnection.Close();

                return res;
            }
        }
    }
}
