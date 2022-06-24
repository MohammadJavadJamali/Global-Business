using System;
using Domain.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Application.Repository
{
    public class TransactionService : ITransaction
    {

        private readonly IRepository<Transaction> _repository;

        public TransactionService(IRepository<Transaction> repository)
        {
            _repository = repository;
        }

        public virtual async Task CreateAsync(Transaction entity)
        {
            entity.TransactionDate = DateTime.Now;
            await _repository.CreateAsync(entity);
        }
        public void Create(Transaction entity)
        {
            entity.TransactionDate = DateTime.Now;
            _repository.Create(entity);
        }

        public async Task<IEnumerable<Transaction>> GetAll(
            Expression<Func<Transaction, bool>> expression = null,
            Expression<Func<Transaction, object>> include = null) =>
            await _repository.GetAll(expression, include);

        public async Task<Transaction> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}
