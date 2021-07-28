#region using
using Dapper;
using MediatR;
using System.Data;
using System.Linq;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Persistance;
using Microsoft.EntityFrameworkCore;
#endregion

namespace Application.FinancialPackages
{
    public class GetAllFinancialPackagesAsync
    {
        public record Query() : IRequest<List<FinancialPackage>>;


        public class Handler : IRequestHandler<Query, List<FinancialPackage>>
        {
            #region Ctor
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            #endregion

            public async Task<List<FinancialPackage>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context
                    .FinancialPackages
                    .AsNoTracking()
                    .ToListAsync();
            }
        }
    }
}
