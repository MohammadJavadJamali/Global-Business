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

namespace Application.Profits
{
    public class GetAllProfitsAsync
    {
        public record Query : IRequest<List<Profit>>;

        public class Handler : IRequestHandler<Query, List<Profit>>
        {
            #region Ctor
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            #endregion

            public async Task<List<Profit>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Profits.AsNoTracking().ToListAsync();
            }
        }
    }
}
