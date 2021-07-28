#region using
using Dapper;
using MediatR;
using System.Data;
using System.Linq;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistance;
#endregion

namespace Application.Users
{
    public class FindUserByIntroductionCodeAsync
    {
        public record Query(string IntroductionCode) : IRequest<AppUser>;

        public class Handler : IRequestHandler<Query, AppUser>
        {
            #region Ctor
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            #endregion

            public async Task<AppUser> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context
                    .Users
                    .Include(n => n.Node)
                    .FirstOrDefaultAsync(u => u.IntroductionCode == request.IntroductionCode);
            }
        }
    }
}
