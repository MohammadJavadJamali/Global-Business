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
#endregion

namespace Application.UserFinancialPackages
{
    public class CreateUserFinancialPackageAsync
    {
        public record Command(UserFinancialPackage UserFinancialPackage) : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            #region Ctor
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            #endregion

            public Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {

                _context.UserFinancialPackages.Add(request.UserFinancialPackage);

                _context.SaveChanges();

                return Unit.Task;
            }
        }
    }
}
