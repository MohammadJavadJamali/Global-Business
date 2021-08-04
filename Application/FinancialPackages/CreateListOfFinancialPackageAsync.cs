using Dapper;
using MediatR;
using System.Linq;
using System.Data;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Application.FinancialPackages
{
    public class CreateListOfFinancialPackageAsync
    {

        public record Command(List<FinancialPackage> FinancialPackages) : IRequest<int>;

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

                var sql = "INSERT INTO FinancialPackages (ProfitPercent, Term, IsDeleted) " +
                   "VALUES(@ProfitPercent, @Term, @IsDeleted)" ;

                //FinancialPackage[] financialPackages = request.FinancialPackages.ToArray();

                _dbConnection.Open();

                var res = await _dbConnection.ExecuteAsync(sql, request.FinancialPackages);

                _dbConnection.Close();

                return res;
            }
        }
    }
}
