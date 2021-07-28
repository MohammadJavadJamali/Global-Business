#region using
using Dapper;
using MediatR;
using System.Data;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
using Persistance;
#endregion

namespace Application.Nodes
{
    public class CreateNodeAsync
    {
        public record Command(Node Node) : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            #region mCtor
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            #endregion

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await _context
                    .Nodes
                    .AddAsync(request.Node);

                return Unit.Value;
            }
        }
    }
}
