using System;
using Domain.Model;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Application.Repository
{
    public class NodeService : INode
    {
        private readonly IRepository<Node> _repository;

        public NodeService(IRepository<Node> repository)
        {
            _repository = repository;
        }
        

        public IEnumerable<Node> Where(Expression<Func<Node, bool>> expression)
        {
            return _repository.Where(expression);
        }

        public virtual async Task CreateAsync(Node entity)
        {
            await _repository.CreateAsync(entity);
        }

        public async Task Create(Node entity)
        {
            await _repository.Create(entity);
        }

        public async Task UpdateAsync(Node entity)
        {
            await _repository.UpdateAsync(entity);
        }
        public void Update(Node entity)
        {
            _repository.Update(entity);
        }

        public async Task<IEnumerable<Node>> GetAll(
            Expression<Func<Node, bool>> expression = null,
            Expression<Func<Node, object>> include = null)
        {
            return await _repository.GetAll(expression, include);
        }

        public async Task<Node> GetByUserId(string userId)
        {
            return await _repository.GetByIdAsync(userId);
        }

        public virtual async Task<Node> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public virtual async Task<Node> GetByParentIdAsync(string id)
        {
            if (id == null)
            {
                return null;
            }
            return await _repository.FirstOrDefaultAsync(u => u.ParentId == id, x => x.AppUser);
        }

        public virtual async Task<Node> FirstOrDefaultAsync(
            Expression<Func<Node, bool>> expression
            , Expression<Func<Node, object>> include = null) =>
            await _repository.FirstOrDefaultAsync(expression, include);

    }
}
