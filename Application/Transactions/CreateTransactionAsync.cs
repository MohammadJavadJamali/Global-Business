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

namespace Application.Transactions
{
    public class CreateTransactionAsync
    {
        public record Command(Transaction Transaction) : IRequest;

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
                await _context.Transactions.AddAsync(request.Transaction);

                await _context.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
