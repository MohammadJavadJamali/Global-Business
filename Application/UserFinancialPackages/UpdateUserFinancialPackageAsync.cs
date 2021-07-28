#region using
using Dapper;
using MediatR;
using System.Data;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Persistance;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
#endregion

namespace Application.UserFinancialPackages
{
    public class UpdateUserFinancialPackageAsync
    {
        public record Command(UserFinancialPackage UserFinancialPackages) : IRequest;

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

                var userFinancialPackage = await _context
                    .UserFinancialPackages
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.UserId == request.UserFinancialPackages.UserId 
                                          && x.FinancialPackageId == request.UserFinancialPackages.FinancialPackageId);

                _mapper.Map(request.UserFinancialPackages, userFinancialPackage);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
