#region using
using Dapper;
using System;
using MediatR;
using System.Data;
using System.Linq;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Persistance;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
#endregion

namespace Application.FinancialPackages
{
    public class WhereUserFinancialPackage
    {
        public record Query(Expression<Func<UserFinancialPackage, bool>> expression) : IRequest<List<UserFinancialPackage>>;

        public class Handler : IRequestHandler<Query, List<UserFinancialPackage>>
        {
            #region Ctor
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            #endregion

            public async Task<List<UserFinancialPackage>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.UserFinancialPackages.Where(request.expression).ToListAsync();
            }
        }
    }
}
