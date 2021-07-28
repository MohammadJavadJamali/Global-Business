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
using AutoMapper;
#endregion

namespace Application.FinancialPackages
{
    public class UpdateFinancialPackagesAsync
    {
        public record Command(FinancialPackage financialPackage) : IRequest;

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
                var entity = await _context.FinancialPackages.FindAsync(request.financialPackage.Id);

                _mapper.Map(request.financialPackage, entity);

                await _context.SaveChangesAsync();

                return Unit.Value;

            }
        }
    }
}
