using System;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Persistence.Repository
{
    public interface IRepository<T> : IDisposable where T : class
    {
                                     

        T Update(T entity);

        Task<T> CreateAsync(T entity);

        Task<T> GetByIdAsync(object id);

        ///remove record from database
        Task<bool> DeleteAsync(object id);

        Task<bool> DeleteAsync(T entity);

        IEnumerable<T> Where(Expression<Func<T, bool>> expression);

        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>> criteria);

        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null
            , Expression<Func<T, object>> expression = null);
    }
}
