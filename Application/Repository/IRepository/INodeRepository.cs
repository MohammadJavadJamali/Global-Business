using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public interface INodeRepository
    {
        Task CreateAsync(Node entity);
        Task<IEnumerable<Node>> GetAll(Expression<Func<Node, bool>> expression = null);
        Task<Node> GetByIdAsync(int id);
        Task<Node> GetByParentIdAsync(string id);
    }
}