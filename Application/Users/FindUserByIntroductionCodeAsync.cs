#region using
using Dapper;
using MediatR;
using System.Data;
using System.Linq;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
#endregion

namespace Application.Users
{
    public class FindUserByIntroductionCodeAsync
    {
        public record Query(string IntroductionCode) : IRequest<AppUser>;

        public class Handler : IRequestHandler<Query, AppUser>
        {
            #region ctor
            private readonly IDbConnection _dbConnection;
            public Handler(IDbConnection dbConnection)
            {
                _dbConnection = dbConnection;
            }
            #endregion

            public async Task<AppUser> Handle(Query request, CancellationToken cancellationToken)
            {
                var sql = "SELECT * FROM AspNetUsers AS A INNER JOIN Nodes AS B ON A.Id = B.UserId";

                _dbConnection.Open();

                var user = await _dbConnection.QueryAsync<AppUser, Node, AppUser>(
                        sql,
                        (appuser, node) =>
                        {
                            appuser.Node = node;
                            return appuser;
                        },
                        splitOn: "Id");

                _dbConnection.Close();

                return user.FirstOrDefault(x => x.IntroductionCode == request.IntroductionCode);
            }
        }
    }
}
