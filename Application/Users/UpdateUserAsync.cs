#region usings
using Dapper;
using MediatR;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using Persistance;
using AutoMapper;
#endregion

namespace Application.Users
{
    public class UpdateUserAsync
    {
        public record Command(AppUser User) : IRequest;

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
                _context.Users.Update(request.User);

                return Unit.Task;
            }
        }
    }
}
