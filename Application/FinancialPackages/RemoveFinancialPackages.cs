#region using
using Dapper;
using MediatR;
using Persistance;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
#endregion

namespace Application.FinancialPackages
{
    public class RemoveFinancialPackages
    {
        public record Command(int Id) : IRequest;

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
                var entity = await _context.FinancialPackages.FindAsync(request.Id);

                _context.FinancialPackages.Remove(entity);

                return Unit.Value;
            }
        }
    }
}
