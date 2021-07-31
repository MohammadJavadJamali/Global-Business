#region using
using Dapper;
using MediatR;
using System.Linq;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Collections;
using Persistance;
#endregion

namespace Application.Users
{
    public class GetAllUsersAsync
    {
        public record Query : IRequest<List<AppUser>>;

        public class Handler : IRequestHandler<Query, List<AppUser>>
        {
            #region Ctor
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            #endregion

            public async Task<List<AppUser>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context
                    .Users
                    .Include(uf => uf.UserFinancialPackages)
                    .Include(u => u.UserFinancialPackages)
                    .ToListAsync();
            }
        }
    }
}
