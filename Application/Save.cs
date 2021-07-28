using MediatR;
using Persistance;
using System.Threading;
using System.Threading.Tasks;

namespace Application
{
    public class Save
    {
        public record Command() : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            #region ctor
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            #endregion

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
