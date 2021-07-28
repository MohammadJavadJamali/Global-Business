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
    public class GetAllNodesAsync
    {
        public record Query : IRequest<List<Node>>;

        public class Handler : IRequestHandler<Query, List<Node>>
        {
            #region Ctor
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            #endregion

            public async Task<List<Node>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Nodes.AsNoTracking().ToListAsync();
            }
        }
    }
}
