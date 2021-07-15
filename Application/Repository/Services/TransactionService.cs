//Remember that the DepositProfit class does not use repository
using System;
using Domain.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;

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

        public async Task<IEnumerable<Transaction>> GetAll(Expression<Func<Transaction, bool>> expression = null) =>
            await _repository.GetAll(expression);

        public async Task<Transaction> GetByIdAsync(int id)
        {
            if (id == 0)
                return null;
            return await _repository.GetByIdAsync(id);
        }

        #endregion
    }
}
