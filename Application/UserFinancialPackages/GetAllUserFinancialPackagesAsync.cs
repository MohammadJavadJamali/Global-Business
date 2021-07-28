#region using
using Dapper;
using MediatR;
using System.Data;
using System.Linq;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistance;
#endregion

namespace Application.Nodes
{
    public class GetAllUserFinancialPackagesAsync
    {
        public record Query : IRequest<List<UserFinancialPackage>>;

        public class Handler : IRequestHandler<Query, List<UserFinancialPackage>>
        {
            #region Ctor
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            #endregion

            public async Task<List<UserFinancialPackage>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.UserFinancialPackages.AsNoTracking().ToListAsync();
            }
        }
    }
}
