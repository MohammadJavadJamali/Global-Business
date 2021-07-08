using System;
using Domain.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Persistence.Repository
{
    public class TransactionService : ITransaction
    {
        #region constructor and fields

        private readonly IRepository<Transaction> _repository;
        public TransactionService(IRepository<Transaction> repository)
        {
            _repository = repository;
        }

        #endregion

        #region Methods

        public virtual async Task CreateAsync(Transaction entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            entity.TransactionDate = DateTime.Now;
            await _repository.CreateAsync(entity);
        }

        public IEnumerable<Transaction> GetAll()
        {
            return _repository.GetAll();
        }

        public async Task<Transaction> GetByIdAsync(int id)
        {
            if (id == 0)
                return null;
            return await _repository.GetByIdAsync(id);
        }

        #endregion
    }
}
