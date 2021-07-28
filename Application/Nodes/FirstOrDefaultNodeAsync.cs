#region using
using Dapper;
using System;
using MediatR;
using System.Linq;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using Persistance;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
#endregion

namespace Application.Nodes
{
    public class FirstOrDefaultNodeAsync
    {
        public record Query(Expression<Func<Node, bool>> expression) : IRequest<Node>;

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
                    .FirstOrDefaultAsync(request.expression);
            }
        }
    }
}
