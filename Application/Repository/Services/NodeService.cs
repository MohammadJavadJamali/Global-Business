using Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class NodeService : INodeRepository
    {

        private readonly IRepository<Node> _repository;

        public NodeService(IRepository<Node> repository)
        {
            _repository = repository;
        }


        public virtual async Task CreateAsync(Node entity)
        {
            if (entity == null)
                throw new ArgumentNullException();

            await _repository.CreateAsync(entity);

        }

        public async Task<IEnumerable<Node>> GetAll(Expression<Func<Node, bool>> expression = null)
        {
            return await _repository.GetAll(expression);
        }

        public virtual async Task<Node> GetByIdAsync(int id)
        {
            if (id == 0)
            {
                return null;
            }
            return await _repository.GetByIdAsync(id);
        }

        public virtual async Task<Node> GetByParentIdAsync(string id)
        {
            if (id == null)
            {
                return null;
            }
            return await _repository.FirstOrDefaultAsync(u => u.ParentId == id, null);
        }

    }
}
