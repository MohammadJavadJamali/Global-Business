#region using
using Dapper;
using MediatR;
using System.Data;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
using Persistance;
using Microsoft.EntityFrameworkCore;
#endregion

namespace Application.FinancialPackages
{
    public class FindFinancialPackageByIdAsync
    {
        public record Query(int Id) : IRequest<FinancialPackage>;

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
                return await _context.FinancialPackages.FirstOrDefaultAsync(f => f.Id == request.Id);
            }
        }
    }
}
   