#region using
using Dapper;
using MediatR;
using System.Data;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
using Persistance;
#endregion

namespace Application.Transactions
{
    public class FindTransactionByIdAsync
    {
        public record Query(int Id) : IRequest<Transaction>;

        public class Handler : IRequestHandler<Query, Transaction>
        {
            #region Ctor
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }
            #endregion

            public async Task<Transaction> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context.Transactions.FindAsync(request.Id);
            }
        }
    }
}
