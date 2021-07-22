using System;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Application.Repository
{
    public interface IRepository<T> : IDisposable where T : class
    {
                                     

        Task<T> UpdateAsync(T entity);
        void Update(T entity);

        Task<T> CreateAsync(T entity);
        Task Create(T entity);

        Task<T> GetByIdAsync(object id);

        ///remove record from database
        Task<bool> DeleteAsync(object id);

        Task<bool> DeleteAsync(T entity);

        IEnumerable<T> Where(Expression<Func<T, bool>> expression);

        Task<T> FirstOrDefaultAsync(
            Expression<Func<T, bool>> expression = null
          , Expression<Func<T, object>> includes = null);

        Task<IEnumerable<T>> GetAll(
            Expression<Func<T, bool>> filter = null
          , Expression<Func<T, object>> includes = null);
    }
}
