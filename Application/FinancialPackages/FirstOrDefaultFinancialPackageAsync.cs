#region using
using Dapper;
using System;
using MediatR;
using System.Data;
using System.Linq;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
using Persistance;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
#endregion

namespace Application.FinancialPackages
{
    public class FirstOrDefaultFinancialPackageAsync
    {
        public record Query(Expression<Func<FinancialPackage, bool>> expression) : IRequest<FinancialPackage>;

        public class Handler : IRequestHandler<Query, FinancialPackage>
        {

            #region Ctor
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            #endregion

            public async Task<FinancialPackage> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context
                    .FinancialPackages
                    .FirstOrDefaultAsync(request.expression);
            }
        }
    }
}
