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

namespace Application.Nodes
{
    public class FindNodeByIdAsync
    {
        public record Query(int Id) : IRequest<Node>;

        public class Handler : IRequestHandler<Query, Node>
        {
            #region Ctor
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            #endregion

            public async Task<Node> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context
                    .Nodes
                    .Include(n => n.AppUser)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(n => n.Id == request.Id);
            }
        }
    }
}
