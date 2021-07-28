#region using
using Dapper;
using MediatR;
using System.Data;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
#endregion

namespace Application.Nodes
{
    public class FindNodeByIdAsync
    {
        public record Query(int Id) : IRequest<Node>;

        public class Handler : IRequestHandler<Query, Node>
        {
            #region ctor
            private readonly IDbConnection _dbConnection;
            public Handler(IDbConnection dbConnection)
            {
                _dbConnection = dbConnection;
            }
            #endregion

            public async Task<Node> Handle(Query request, CancellationToken cancellationToken)
            {
                var sql = "SELECT * FROM Nodes WHERE Id = @Id";

                _dbConnection.Open();

                var node = await _dbConnection.QueryFirstOrDefaultAsync<Node>(sql, new { Id = request.Id });
                
                _dbConnection.Close();

                return node;
            }
        }
    }
}
