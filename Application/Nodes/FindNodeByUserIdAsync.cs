﻿#region usings
using Dapper;
using MediatR;
using Domain.Model;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Data;
#endregion

namespace Application.Nodes
{
    public class FindNodeByUserIdAsync
    {
        public record Query(string Id) : IRequest<Node>;

        public class Handler : IRequestHandler<Query, Node>
        {
            #region Ctor
            private readonly IDbConnection _dbConnection;
            public Handler(IDbConnection dbConnection)
            {
                _dbConnection = dbConnection;
            }
            #endregion

            public async Task<Node> Handle(Query request, CancellationToken cancellationToken)
            {
                #region sql
                var sql = "SELECT " +
                    "Nodes.Id, Nodes.ParentId, Nodes.LeftUserId, Nodes.RightUserId, Nodes.TotalMoneyInvested, " +
                    "Nodes.TotalMoneyInvestedBySubsets, Nodes.MinimumSubBrachInvested, Nodes.IntroductionCode, " +
                    "Nodes.IsCalculate, Nodes.UserId, AspNetUsers.Id, AspNetUsers.IsDeleted," +
                    "AspNetUsers.LastName, AspNetUsers.FirstName, AspNetUsers.CommissionPaid, AspNetUsers.RegisterDate," +
                    "AspNetUsers.AccountBalance, AspNetUsers.IntroductionCode ,AspNetUsers.UserName,AspNetUsers.Email, " +
                    "AspNetUsers.EmailConfirmed, AspNetUsers.PasswordHash, AspNetUsers.PhoneNumber, " +
                    "AspNetUsers.PhoneNumberConfirmed, AspNetUsers.TwoFactorEnabled, AspNetUsers.LockoutEnd," +
                    "AspNetUsers.LockoutEnabled, AspNetUsers.AccessFailedCount" +
                    " FROM Nodes JOIN AspNetUsers ON nodes.UserId = aspnetusers.id";
                #endregion

                _dbConnection.Open();

                var nodes = await _dbConnection.QueryAsync<Node, AppUser, Node>(
                    sql,
                    (node, appuser) =>
                    {
                        node.AppUser = appuser;
                        node.UserId = appuser.Id;
                        appuser.Node = node;
                        return node;
                    },
                    splitOn: "UserId");

                _dbConnection.Close();

                return nodes.FirstOrDefault(n => n.UserId == request.Id);

            }
        }
    }
}
