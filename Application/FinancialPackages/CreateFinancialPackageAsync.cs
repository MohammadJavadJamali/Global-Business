#region using
using Dapper;
using MediatR;
using System.Data;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
using Persistance;
#endregion

namespace Application.FinancialPackages
{
    public class CreateFinancialPackageAsync
    {
        public record Command(FinancialPackage FinancialPackage) : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            #region Ctor
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            #endregion

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await _context.AddAsync(request.FinancialPackage);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
