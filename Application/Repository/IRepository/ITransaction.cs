using System;
using Domain.Model;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Persistence.Repository
{
    public interface ITransaction
    {
        Task CreateAsync(Transaction entity);
        Task<IEnumerable<Transaction>> GetAll(Expression<Func<Transaction, bool>> expression = null);
        Task<Transaction> GetByIdAsync(int id);
    }
}