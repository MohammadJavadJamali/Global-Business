#region usings
using Dapper;
using MediatR;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
#endregion

namespace Application.Users
{
    public class UpdateUserAsync
    {
        public record Command(AppUser User) : IRequest;

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
                var sql = "UPDATE AspNetUsers SET " +
                    "FirstName = @FirstName, LastName = @LastName, AccountBalance = CONVERT(decimal(18, 4), @AccountBalance), " +
                    "IsDeleted = @IsDeleted, UserName = @UserName, Email = @Email, EmailConfirmed = @EmailConfirmed, " +
                    "PhoneNumber = @PhoneNumber, PhoneNumberConfirmed = @PhoneNumberConfirmed, TwoFactorEnabled = @TwoFactorEnabled, " +
                    "LockoutEnd = @LockoutEnd, LockoutEnabled = @LockoutEnabled, AccessFailedCount = @AccessFailedCount, " +
                    "IntroductionCode = @IntroductionCode, CommissionPaid = @CommissionPaid WHERE Id = @Id";
                #endregion

                #region parameters
                var parameters = new
                {
                    request.User.FirstName,
                    request.User.LastName,
                    request.User.AccountBalance,
                    request.User.IsDeleted,
                    request.User.UserName,
                    request.User.Email,
                    request.User.EmailConfirmed,
                    request.User.PhoneNumber,
                    request.User.PhoneNumberConfirmed,
                    request.User.TwoFactorEnabled,
                    request.User.LockoutEnd,
                    request.User.LockoutEnabled,
                    request.User.AccessFailedCount,
                    request.User.IntroductionCode,
                    request.User.CommissionPaid,
                    request.User.Id
                };
                #endregion

                _dbConnection.Open();

                await _dbConnection.ExecuteAsync(sql: sql, param: parameters);
                
                _dbConnection.Close();

                return Unit.Value;
            }
        }
    }
}
