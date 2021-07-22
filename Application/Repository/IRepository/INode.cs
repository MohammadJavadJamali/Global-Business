using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Repository
{
    public interface INode
    {
        Task CreateAsync(Node entity);
        Task Create(Node entity);

        Task UpdateAsync(Node entity);
        void Update(Node entity);

        IEnumerable<Node> Where(Expression<Func<Node, bool>> expression);

        Task<IEnumerable<Node>> GetAll(
            Expression<Func<Node, bool>> expression = null,
            Expression<Func<Node, object>> include = null);

        Task<Node> FirstOrDefaultAsync(
            Expression<Func<Node, bool>> expression
          , Expression<Func<Node, object>> include = null);

        Task<Node> GetByIdAsync(int id);

        Task<Node> GetByUserId(string userId);

        Task<Node> GetByParentIdAsync(string id);

    }
}