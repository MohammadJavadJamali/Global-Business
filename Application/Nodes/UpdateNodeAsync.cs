#region usings
using Dapper;
using MediatR;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Z.Dapper.Plus;
using Persistance;
using System.Data;
using AutoMapper;
#endregion

namespace Application.Nodes
{
    public class UpdateNodeAsync
    {
        public record Command(Node Node) : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            #region Ctor
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            #endregion

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = await _context.Nodes.FindAsync(request.Node.Id);

                _mapper.Map(request.Node, entity);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
