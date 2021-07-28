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

            public Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                _context.UserFinancialPackages.Update(request.UserFinancialPackages);

                return Unit.Task;
            }
        }
    }
}
