#region using
using Dapper;
using MediatR;
using System.Linq;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Collections;
#endregion

namespace Application.Users
{
    public class GetAllUsersAsync
    {
        public record Query : IRequest<List<AppUser>>;

        public class Handler : IRequestHandler<Query, List<AppUser>>
        {
            #region Ctor
            private readonly IDbConnection _dbConnection;
            public Handler(IDbConnection dbConnection)
            {
                _dbConnection = dbConnection;
            }
            #endregion

            public async Task<List<AppUser>> Handle(Query request, CancellationToken cancellationToken)
            {
                #region sql
                var sql = 
                    "SELECT * FROM AspNetUsers AS A" +
                    " INNER JOIN UserFinancialPackages AS B " +
                    "ON A.Id = B.UserId";
                #endregion

                var orderDectionary = new Dictionary<string, AppUser>();

                _dbConnection.Open();

                var users = await _dbConnection.QueryAsync<AppUser, UserFinancialPackage, AppUser>(
                    sql,
                    (appuser, userFinancialPackage) =>
                    {
                        AppUser appuserEntry;
                        if (!orderDectionary.TryGetValue(appuser.Id, out appuserEntry))
                        {
                            appuserEntry = appuser;
                            appuserEntry.UserFinancialPackages = new List<UserFinancialPackage>();
                            orderDectionary.Add(appuserEntry.Id, appuserEntry);
                        }

                        appuserEntry.UserFinancialPackages.Add(userFinancialPackage);
                        return appuserEntry;
                    },
                    splitOn: "UserId");
                
                _dbConnection.Close();

                return users.ToList();

            }
        }
    }
}
