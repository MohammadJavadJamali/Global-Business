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
    public class FindNodeByParentIdAsync
    {
        public record Query(string Id) : IRequest<Node>;

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
                var sql = "SELECT * FROM Nodes WHERE ParentId = @Id";

                _dbConnection.Open();

                var node = await _dbConnection.QueryFirstOrDefaultAsync(sql, new { Id = request.Id });

                _dbConnection.Close();

                return node;

            }
        }
    }
}
