#region using
using Dapper;
using MediatR;
using System.Data;
using System.Linq;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
#endregion

namespace Application.Nodes
{
    public class GetAllNodesAsync
    {
        public record Query : IRequest<List<Node>>;

        public class Handler : IRequestHandler<Query, List<Node>>
        {
            #region ctor
            private readonly IDbConnection _dbConnection;
            public Handler(IDbConnection dbConnection)
            {
                _dbConnection = dbConnection;
            }
            #endregion

            public async Task<List<Node>> Handle(Query request, CancellationToken cancellationToken)
            {
                var sql = "SELECT * FROM Nodes";

                _dbConnection.Open();

                var nodes = (await _dbConnection.QueryAsync<Node>(sql)).ToList();

                _dbConnection.Close();

                return nodes;
            }
        }
    }
}
