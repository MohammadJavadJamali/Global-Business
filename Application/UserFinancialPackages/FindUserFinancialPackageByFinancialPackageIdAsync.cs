#region using
using Dapper;
using MediatR;
using System.Data;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistance;
#endregion

namespace Application.UserFinancialPackages
{
    /// <summary>
    /// Find a UserFinancialPackage by FinancialPackageId
    /// </summary>
    public class FindUserFinancialPackageByFinancialPackageIdAsync
    {
        public record Query(int Id) : IRequest<UserFinancialPackage>;

        public class Handler : IRequestHandler<Query, UserFinancialPackage>
        {
            #region Ctor
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            #endregion

            public async Task<UserFinancialPackage> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.UserFinancialPackages.FindAsync(request.Id);
            }
        }
    }
}
