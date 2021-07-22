using System;
using Domain.Model;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Application.Repository
{
    public interface ITransaction
    {
        Task CreateAsync(Transaction entity);

        void Create(Transaction entity);

        Task<IEnumerable<Transaction>> GetAll(
            Expression<Func<Transaction, bool>> expression = null,
            Expression<Func<Transaction, object>> include = null);

        Task<Transaction> GetByIdAsync(int id);

    }
}