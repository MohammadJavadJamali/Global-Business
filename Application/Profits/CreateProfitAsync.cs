#region using
using Dapper;
using MediatR;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using Persistance;
#endregion

namespace Application.Profits
{
    public class CreateProfitAsync
    {
        public record Command(Profit Profits) : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            #region Ctor
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            #endregion

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await _context.Profits.AddAsync(request.Profits);

                return Unit.Value;
            }
        }
    }
}
